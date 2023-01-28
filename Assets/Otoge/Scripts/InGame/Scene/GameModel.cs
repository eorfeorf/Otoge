using System;
using System.Collections.Generic;
using UniRx;
using UnityEngine;
using Unit = UniRx.Unit;

/// <summary>
/// ゲーム中の
/// </summary>
public class GameModel
{
    /// <summary>
    /// 入力適用イベント.
    /// </summary>
    public IReadOnlyReactiveProperty<(Note, GameDefine.JudgeRank)> OnApplyRank => inGamePlayHandler.OnApplyRank;
    /// <summary>
    /// 経過時間更新.
    /// </summary>
    public IReadOnlyReactiveProperty<float> OnProgressTime => progressTimer.OnProgress;
    /// <summary>
    /// ノーツが通り過ぎたイベント.
    /// </summary>
    public IObservable<Note> OnPassNote => onPassNote;
    private readonly ISubject<Note> onPassNote = new Subject<Note>();
    /// <summary>
    /// コンボ数変化.
    /// </summary>
    public IReadOnlyReactiveProperty<int> OnChangedCombo => combo.Count;
    /// <summary>
    /// リセット.
    /// </summary>
    public IReadOnlyReactiveProperty<Unit> OnReset => onReset;
    private readonly ReactiveProperty<Unit> onReset = new ReactiveProperty<Unit>();
    
    // ゲームプレイ.
    private bool isStart = false;
    private readonly Combo combo = new();

    private NoteContainer noteContainer;
    private InGamePlayHandler inGamePlayHandler;
    private ProgressTimer progressTimer;

    public GameModel(Action<IList<Note>> onInitialize, CompositeDisposable disposable)
    {
        // ノーツデータの生成.
        noteContainer = new NoteContainer();

        // 時間更新タイマー.
        progressTimer = new ProgressTimer(disposable);
        
        // レーンに対して操作を適用.
        inGamePlayHandler = new InGamePlayHandler(noteContainer, progressTimer);
        
        // ノーツランク適用.
        inGamePlayHandler.OnApplyRank.SkipLatestValueOnSubscribe().Subscribe(tuple =>
        {
            combo.Add();
        }).AddTo(disposable);
        
        // 時間更新.
        progressTimer.OnProgress.Subscribe(progressTime =>
        {
            // 通り過ぎた.
            var passedNote = CheckPassNote(noteContainer.Notes, progressTime);
            if (passedNote != null)
            {
                // 削除.
                passedNote.Active = false;
                combo.Reset();
                onPassNote.OnNext(passedNote);
            }
        }).AddTo(disposable);

        // 更新.
        Observable.EveryUpdate().Subscribe(_ =>
        {
            if (Input.GetKeyDown(KeyCode.R))
            {
                Reset();
            }
        }).AddTo(disposable);
        
        // 初期化完了通知.
        Debug.Log("[GameModel] Initialized.");
        onInitialize(noteContainer.Notes);
    }

    /// <summary>
    /// ゲーム開始.
    /// </summary>
    public void Play()
    {
        progressTimer.Start();
        isStart = true;
    }

    /// <summary>
    /// 開始前にリセット.
    /// </summary>
    public void Reset()
    {
        progressTimer.Start();
        noteContainer.AllActive();
        onReset.SetValueAndForceNotify(Unit.Default);
    }

    /// <summary>
    /// ノーツが通り過ぎたか.
    /// </summary>
    private Note CheckPassNote(IList<Note> notes, float progressTime)
    {
        foreach (var note in notes)
        {
            if (!note.Active)
            {
                continue;
            }
            
            var passTime = note.Time + GameDefine.TimingGood;
            if (passTime < progressTime)
            {
                return note;
            }
        }

        return null;
    }
}