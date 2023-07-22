using System.Collections.Generic;
using System.Linq;
using UniRx;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace Otoge.Domain
{
    /// <summary>
    /// インゲームのロジック管理.
    /// </summary>
    public class InGamePlayer : IInitializable
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
        private readonly ReactiveProperty<NoteApplyData> onPassNote = new();

        private readonly InputCommand _inputCommand;
        private readonly NoteContainer _noteContainer;
        private readonly LifeCycle _lifeCycle;  

        public void Initialize()
        {
            Debug.Log("");
        }

        [Inject]
        public InGamePlayer(NoteContainer noteContainer, ProgressTimer progressTimer, InputCommand inputCommand, LifeCycle lifeCycle)
        {
            _lifeCycle = lifeCycle;
            _inputCommand = inputCommand;
            _noteContainer = noteContainer;
        
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
            }).AddTo(_lifeCycle.CompositeDisposable);
            
            Debug.Log("[InGamePlayer] Initialized.");
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
            _noteContainer.SetActive(note, false);
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
            _inputCommand.Tap.SkipLatestValueOnSubscribe().Subscribe(data =>
            {
                // そのレーンの生きてるノーツだけ取得.
                var note = _noteContainer.Notes.FirstOrDefault(n => n.Value.Active && n.Value.Lane == data.Lane).Value;

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
            }).AddTo(_lifeCycle.CompositeDisposable);

            // 長押し.
            _inputCommand.Hold.SkipLatestValueOnSubscribe().Subscribe(data =>
            {
                // そのレーンの生きてるノーツだけ取得.
                var note = _noteContainer.Notes.FirstOrDefault(n => n.Value.Lane == data.Lane && n.Value.Active).Value;

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
            }).AddTo(_lifeCycle.CompositeDisposable);
        }
    }
}