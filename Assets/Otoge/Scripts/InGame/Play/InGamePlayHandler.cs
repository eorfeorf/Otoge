using System.Collections.Generic;
using System.Linq;
using UniRx;

/// <summary>
/// インゲームのロジック管理.
/// </summary>
public class InGamePlayHandler
{
    /// <summary>
    /// 入力適用イベント.
    /// </summary>
    public IReadOnlyReactiveProperty<ApplyNoteData> OnApplyNote => onApplyNote;
    private readonly ReactiveProperty<ApplyNoteData> onApplyNote = new();

    /// <summary>
    /// ノーツが通り過ぎたイベント.
    /// </summary>
    public IReadOnlyReactiveProperty<Note> OnPassNote => onPassNote;
    private ReactiveProperty<Note> onPassNote = new();

    private InputEventFactory inputEventFactory;
    private InputCommand inputCommand;
    private NoteContainer noteContainer;
    private CompositeDisposable disposable = new CompositeDisposable();

    public InGamePlayHandler(NoteContainer noteContainer, ProgressTimer progressTimer)
    {
        inputEventFactory = new InputEventFactory(disposable);
        inputCommand = new InputCommand(inputEventFactory.InputEvent);
        this.noteContainer = noteContainer;

        inputCommand.Tap.SkipLatestValueOnSubscribe().Subscribe(data =>
        {
            // そのレーンの生きてるノーツだけ取得.
            //var note = noteContainer.Notes.First(note => note.Value.Active && note.Value.Lane == data.Lane).Value;
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

        inputCommand.Hold.SkipLatestValueOnSubscribe().Subscribe(data =>
        {
            // そのレーンの生きてるノーツだけ取得.
            // 
            var note = noteContainer.Notes.FirstOrDefault(note => note.Value.Lane == data.Lane && note.Value.Active).Value;

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

        // 時間更新.
        progressTimer.OnProgress.Subscribe(progressTime =>
        {
            // 通り過ぎた.
            var passedNotes = GetPassNote(noteContainer.Notes.Values, progressTime);
            foreach (var note in passedNotes)
            {
                // 削除.
                noteContainer.SetActive(note, false);
                onPassNote.SetValueAndForceNotify(note);
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
        var data = new ApplyNoteData()
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
}