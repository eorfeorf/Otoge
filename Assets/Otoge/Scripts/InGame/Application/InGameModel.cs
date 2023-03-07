using System;
using System.Collections.Generic;
using Otoge.Scripts.InGame.Domain;
using UniRx;
using UnityEngine;
using Unit = UniRx.Unit;

/// <summary>
/// Viewを初期化するのに必要なデータ.
/// </summary>
public class ViewInitializeData
{
    /// <summary>
    /// 生成されたノーツ.
    /// </summary>
    public ICollection<Note> Notes { get; private set; }
    /// <summary>
    /// 最大レーン数.
    /// </summary>
    public int MaxLaneNum { get; private set; }
    /// <summary>
    /// BPM.
    /// </summary>
    public float Bpm { get; private set; }

    public ViewInitializeData(ICollection<Note> notes, int maxLaneNum, float bpm)
    {
        Notes = notes;
        MaxLaneNum = maxLaneNum;
        Bpm = bpm;
    }
}

/// <summary>
/// インゲームの進行を管理するクラス.
/// </summary>
public class InGameModel
{
    /// <summary>
    /// 入力適用イベント.
    /// </summary>
    public IReadOnlyReactiveProperty<NoteApplyData> OnApplyNote => inGamePlayer.OnApplyNote;
    /// <summary>
    /// ノーツが通り過ぎたイベント.
    /// </summary>
    public IReadOnlyReactiveProperty<NoteApplyData> OnPassNote => inGamePlayer.OnPassNote;
    /// <summary>
    /// 経過時間更新.
    /// </summary>
    public IReadOnlyReactiveProperty<float> OnProgressTime => progressTimer.OnProgress;
    /// <summary>
    /// コンボ数変化.
    /// </summary>
    public IReadOnlyReactiveProperty<int> OnChangedCombo => combo.Count;
    /// <summary>
    /// コンボ数変化.
    /// </summary>
    public IReadOnlyReactiveProperty<int> OnChangedScore => score.Count;
    /// <summary>
    /// リセット.
    /// </summary>
    public IReadOnlyReactiveProperty<Unit> OnReset => onReset;
    private readonly ReactiveProperty<Unit> onReset = new ReactiveProperty<Unit>();
    

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
    /// スコア.
    /// </summary>
    private Score score;
    /// <summary>
    /// スコア計算.
    /// </summary>
    private ScoreCalculator scoreCalculator;

    /// <summary>
    /// ゲームが開始されたか
    /// </summary>
    private bool isStart = false;
    
    public InGameModel(Action<ViewInitializeData> onInitialize, CompositeDisposable disposable)
    {
        noteContainer = new NoteContainer();
        progressTimer = new ProgressTimer(disposable);
        inGamePlayer = new InGamePlayer(noteContainer, progressTimer);
        combo = new Combo();
        score = new Score();
        scoreCalculator = new ScoreCalculator();
        
        // ノーツランク適用.
        inGamePlayer.OnApplyNote.SkipLatestValueOnSubscribe().Subscribe(data =>
        {
            // コンボ加算.
            combo.Add();
            // スコア加算.
            score.Add(scoreCalculator.Calc(data.Rank));
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

        var viewInitializeData = new ViewInitializeData(noteContainer.Notes.Values, GameDefine.LANE_NUM, GameDefine.BPM);
        onInitialize(viewInitializeData);
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