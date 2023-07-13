using System.Collections.Generic;
using System.Linq;
using UniRx;

namespace Otoge.Domain.Otoge.Scripts.Runtime.Domain.Play
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
        private NoteContainer noteContainer;
        private CompositeDisposable disposable = new CompositeDisposable();

        public InGamePlayer(NoteContainer noteContainer, ProgressTimer progressTimer)
        {
            inputEventFactory = new InputEventFactory(disposable);
            inputCommand = new InputCommand(inputEventFactory.InputEvent);
            this.noteContainer = noteContainer;
        
            InitInput(progressTimer);

            // 時間更新.
            progressTimer.OnProgress.Subscribe(progressTime =>
            {
                // 通り過ぎた.
                var passedNotes = GetPassNote(noteContainer.Notes.Values, progressTime);
                foreach (var note in passedNotes)
                {
                    // 削除.
                    noteContainer.SetActive(note, false);
                    var data = new NoteApplyData()
                    {
                        Note = note,
                        Rank = GameDefine.JudgeRank.Miss,
                    };
                    onPassNote.SetValueAndForceNotify(data);
                }
            }).AddTo(disposable);
        }

        /// <summary>
        /// ノーツに適用.
        /// </summary>
        /// <param name="note"></param>
        /// <param name="progressTime"></param>
        private void ApplyCommand(Note note, float progressTime)
        {
            // 叩いた.
            var rank = NoteTiming.CheckRank(note, progressTime);
            noteContainer.SetActive(note, false);
            var data = new NoteApplyData()
            {
                Note = note,
                Rank = rank,
            };
            onApplyNote.SetValueAndForceNotify(data);
        }

        /// <summary>
        /// ノーツが通り過ぎたか.
        /// </summary>
        private List<Note> GetPassNote(ICollection<Note> notes, float progressTime)
        {
            var passedNotes = new List<Note>();
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
                var note = noteContainer.Notes.FirstOrDefault(n => n.Value.Active && n.Value.Lane == data.Lane).Value;

                if (note == null)
                {
                    // 生きてるノーツがない.
                    return;
                }

                if (!NoteTiming.CheckInRangeApplyTime(note, progressTimer.OnProgress.Value))
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
                var note = noteContainer.Notes.FirstOrDefault(n => n.Value.Lane == data.Lane && n.Value.Active).Value;

                if (note == null)
                {
                    // 生きてるノーツがない.
                    return;
                }

                if (!NoteTiming.CheckInRangeApplyTime(note, progressTimer.OnProgress.Value))
                {
                    // 判定時間外.
                    return;
                }

                ApplyCommand(note, progressTimer.OnProgress.Value);
            }).AddTo(disposable);
        }
    }
}