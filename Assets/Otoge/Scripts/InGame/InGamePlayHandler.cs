using System;
using System.Linq;
using UniRx;

public class InGamePlayHandler
{
    /// <summary>
    /// 入力適用イベント.
    /// </summary>
    public IReadOnlyReactiveProperty<(Note, GameDefine.JudgeRank)> OnApplyRank => onApplyRank;
    private readonly ReactiveProperty<(Note, GameDefine.JudgeRank)> onApplyRank= new();
    
    private InputEventFactory inputEventFactory;
    private InputCommand inputCommand;
    private CompositeDisposable disposable = new CompositeDisposable();
    
    public InGamePlayHandler(NoteContainer noteContainer, ProgressTimer progressTimer)
    {
        inputEventFactory = new InputEventFactory(disposable);
        inputCommand = new InputCommand(inputEventFactory.InputEvent);
        
        inputCommand.Tap.SkipLatestValueOnSubscribe().Subscribe(data =>
        {
            // そのレーンの生きてるノーツだけ取得.
            var note = noteContainer.Notes.FirstOrDefault(note => note.Lane == data.Lane && note.Active);
        
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
            var rank = NoteTiming.CheckRank(note, progressTimer.OnProgress.Value);
            noteContainer.SetNoteActive(note, false);
            onApplyRank.SetValueAndForceNotify((note, rank));
        }).AddTo(disposable);

        inputCommand.Hold.SkipLatestValueOnSubscribe().Subscribe(data =>
        {
            
        }).AddTo(disposable);
    }
}
