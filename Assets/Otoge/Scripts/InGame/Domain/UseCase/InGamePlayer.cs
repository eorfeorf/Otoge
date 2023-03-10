using System.Collections.Generic;
using System.Linq;
using Otoge.Scripts.InGame.Application.Interface;
using UniRx;

namespace Otoge.Scripts.InGame.Application
{
    /// <summary>
    /// インゲームのロジック管理.
    /// </summary>
    public class InGamePlayer
    {
        /// <summary>
        /// 入力適用イベント.
        /// </summary>
        public IReadOnlyReactiveProperty<NoteApplyData> OnApplyNote => onApplyNote;
        private readonly ReactiveProperty<NoteApplyData> onApplyNote = new();

        /// <summary>
        /// ノーツが通り過ぎたイベント.
        /// </summary>
        public IReadOnlyReactiveProperty<NoteApplyData> OnPassNote => onPassNote;
        private ReactiveProperty<NoteApplyData> onPassNote = new();

        private InputEventFactory inputEventFactory;
        private InputCommand inputCommand;
        private INoteRepository noteRepository;
        private CompositeDisposable disposable = new CompositeDisposable();

        public InGamePlayer(INoteRepository noteRepository, ProgressTimer progressTimer)
        {
            inputEventFactory = new InputEventFactory(disposable);
            inputCommand = new InputCommand(inputEventFactory.InputEvent);
            this.noteRepository = noteRepository;
        
            InitInput(progressTimer);

            // 時間更新.
            progressTimer.OnProgress.Subscribe(progressTime =>
            {
                // 通り過ぎた.
                var passedNotes = GetPassNote(noteRepository.Notes, progressTime);
                foreach (var note in passedNotes)
                {
                    // 削除.
                    //noteContainer.SetActive(note, false);
                    var data = new NoteApplyData()
                    {
                        NoteData = note,
                        Rank = GameDefine.JudgeRank.Miss,
                    };
                    onPassNote.SetValueAndForceNotify(data);
                }
            }).AddTo(disposable);
        }

        /// <summary>
        /// ノーツに適用.
        /// </summary>
        /// <param name="noteData"></param>
        /// <param name="progressTime"></param>
        private void ApplyCommand(NoteData noteData, float progressTime)
        {
            // 叩いた.
            var rank = NoteTiming.CheckRank(noteData.Time, progressTime);
            //noteContainer.SetActive(note, false);
            var data = new NoteApplyData()
            {
                NoteData = noteData,
                Rank = rank,
            };
            onApplyNote.SetValueAndForceNotify(data);
        }

        /// <summary>
        /// ノーツが通り過ぎたか.
        /// </summary>
        private List<NoteData> GetPassNote(ICollection<NoteData> notes, float progressTime)
        {
            var passedNotes = new List<NoteData>();
            foreach (var note in notes)
            {
                if (!note.Active)
                {
                    continue;
                }

                // 有効判定時間が過ぎた.
                var passTime = note.Time + GameDefine.TimingGood;
                if (passTime < progressTime)
                {
                    passedNotes.Add(note);
                }
            }

            return passedNotes;
        }

        private void InitInput(ProgressTimer progressTimer)
        {
            // タップ.
            inputCommand.Tap.SkipLatestValueOnSubscribe().Subscribe(data =>
            {
                // そのレーンの生きてるノーツだけ取得.
                var note = noteRepository.Notes.FirstOrDefault(n => n.Active && n.Lane == data.Lane);

                if (note == null)
                {
                    // 生きてるノーツがない.
                    return;
                }

                if (!NoteTiming.CheckInRangeApplyTime(note.Type, note.Time, progressTimer.OnProgress.Value))
                {
                    // 判定時間外.
                    return;
                }

                // 叩いた.
                ApplyCommand(note, progressTimer.OnProgress.Value);
            }).AddTo(disposable);

            // 長押し.
            inputCommand.Hold.SkipLatestValueOnSubscribe().Subscribe(data =>
            {
                // そのレーンの生きてるノーツだけ取得.
                var note = noteRepository.Notes.FirstOrDefault(n => n.Lane == data.Lane && n.Active);

                if (note == null)
                {
                    // 生きてるノーツがない.
                    return;
                }

                if (!NoteTiming.CheckInRangeApplyTime(note.Type, note.Time, progressTimer.OnProgress.Value))
                {
                    // 判定時間外.
                    return;
                }

                ApplyCommand(note, progressTimer.OnProgress.Value);
            }).AddTo(disposable);
        }
    }
}