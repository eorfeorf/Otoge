using System;
using System.Collections.Generic;
using UniRx;
using UnityEngine;
using Unit = UniRx.Unit;

/// <summary>
/// インゲームの進行を管理するクラス.
/// </summary>
public class InGameModel
{
    /// <summary>
    /// 入力適用イベント.
    /// </summary>
    public IReadOnlyReactiveProperty<ApplyNoteData> OnApplyNote => inGamePlayer.OnApplyNote;
    /// <summary>
    /// ノーツが通り過ぎたイベント.
    /// </summary>
    public IReadOnlyReactiveProperty<ApplyNoteData> OnPassNote => inGamePlayer.OnPassNote;
    /// <summary>
    /// 経過時間更新.
    /// </summary>
    public IReadOnlyReactiveProperty<float> OnProgressTime => progressTimer.OnProgress;
    /// <summary>
    /// コンボ数変化.
    /// </summary>
    public IReadOnlyReactiveProperty<int> OnChangedCombo => combo.Count;
    /// <summary>
    /// リセット.
    /// </summary>
    public IReadOnlyReactiveProperty<Unit> OnReset => onReset;
    private readonly ReactiveProperty<Unit> onReset = new ReactiveProperty<Unit>();
    

    /// <summary>
    /// ノーツ管理.
    /// </summary>
    private NoteContainer noteContainer;
    /// <summary>
    /// インゲーム管理.
    /// </summary>
    private InGamePlayer inGamePlayer;
    /// <summary>
    /// タイマー.
    /// </summary>
    private ProgressTimer progressTimer;
    /// <summary>
    /// コンボ.
    /// </summary>
    private Combo combo;

    /// <summary>
    /// ゲームが開始されたか
    /// </summary>
    private bool isStart = false;
    
    public InGameModel(Action<ICollection<Note>> onInitialize, CompositeDisposable disposable)
    {
        noteContainer = new NoteContainer();
        progressTimer = new ProgressTimer(disposable);
        combo = new Combo();
        inGamePlayer = new InGamePlayer(noteContainer, progressTimer);
        
        // ノーツランク適用.
        inGamePlayer.OnApplyNote.SkipLatestValueOnSubscribe().Subscribe(data =>
        {
            // コンボ加算.
            combo.Add();
        }).AddTo(disposable);
        
        // ノーツが通り過ぎた.
        inGamePlayer.OnPassNote.SkipLatestValueOnSubscribe().Subscribe(note =>
        {
            // コンボリセット.
            combo.Reset();
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
        onInitialize(noteContainer.Notes.Values);
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
        noteContainer.SetActiveAll(true);
        onReset.SetValueAndForceNotify(Unit.Default);
    }
}