using System;
using System.Collections.Generic;
using System.Linq;
using UniRx;
using UnityEngine;

public class GameModel : IDisposable
{
    /// <summary>
    /// 経過時間更新.
    /// </summary>
    public readonly IReactiveProperty<float> OnProgressTime = new ReactiveProperty<float>();

    /// <summary>
    /// 入力適用イベント.
    /// </summary>
    public IReadOnlyReactiveProperty<(Note, GameDefine.JudgeRank)> OnApplyRank => onApplyRank;
    private readonly ReactiveProperty<(Note, GameDefine.JudgeRank)> onApplyRank= new();
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
    /// 
    /// </summary>
    public IList<Note> Notes => notes;
    
    // ゲームプレイ.
    private List<Note> notes = new List<Note>();
    private float startTime = 0;
    private bool isStart = false;
    private readonly Combo combo = new Combo();

    private AudioSource audioSource = new AudioSource();
    private AudioClip audioClip;

    private CompositeDisposable disposable = new CompositeDisposable();

    public GameModel(IInputEvent input, Action<IList<Note>> onInitialize)
    {
        input.Touch.Subscribe(lane =>
        {
            ApplyLane(lane, notes);
        }).AddTo(disposable);
        // input.Left.Subscribe(_ =>
        // {
        //     ApplyLane(0, notes);   
        // }).AddTo(disposable);
        // input.Right.Subscribe(_ =>
        // {
        //     ApplyLane(1, notes);
        // }).AddTo(disposable);

        // 更新.
        Observable.EveryUpdate().Subscribe(_ =>
        {
            if (!isStart)
            {
                return;
            }

            if (Input.GetKeyDown(KeyCode.R))
            {
                Reset();
            }

            var progressTime = Time.time - startTime;
        
            // 通り過ぎた.
            var passedNote = CheckPassNote(progressTime);
            if (passedNote != null)
            {
                // 削除.
                passedNote.Active = false;
                combo.Reset();
                onPassNote.OnNext(passedNote);
            }

            OnProgressTime.Value = progressTime;
        }).AddTo(disposable);

        // ノーツランク適用.
        onApplyRank.Subscribe(tuple =>
        {
            combo.Add();
        }).AddTo(disposable);
        
        // ノーツデータの生成.
        CreateNoteData(notes);
        // 音の生成.
        CreateApplySe();
        
        // 初期化完了通知.
        Debug.Log("[GameModel] Initialized.");
        onInitialize(notes);
    }

    /// <summary>
    /// 入力適用SEを作成.
    /// </summary>
    private void CreateApplySe()
    {
        int position = 0;
        int samplerate = 44100;
        float frequency = 440;

        void OnAudioRead(float[] data)
        {
            int count = 0;
            while (count < data.Length)
            {
                data[count] = Mathf.Sin(2 * Mathf.PI * frequency * position / samplerate) * 0.1f;
                position++;
                count++;
            }
        }

        void OnAudioSetPosition(int newPosition)
        {
            position = newPosition;
        }
        
        //audioClip = AudioClip.Create("test", samplerate/100, 1, samplerate/10, true, OnAudioRead, OnAudioSetPosition);
        //audioSource = GetComponent<AudioSource>();
        //audioSource.clip = audioClip;
        //audioSource.Play();
    }

    private void Update()
    {
    }

    /// <summary>
    /// ゲーム開始.
    /// </summary>
    public void Play()
    {
        startTime = Time.time;
        isStart = true;
    }

    /// <summary>
    /// 開始前にリセット.
    /// </summary>
    private void Reset()
    {
        startTime = Time.time;
        foreach(var note in notes)
        {
            note.Active = false;
        }
    }

    /// <summary>
    /// ノーツデータの生成.
    /// </summary>
    private void CreateNoteData(ICollection<Note> noteList)
    {
        for (int i = 0; i < 10; ++i)
        {
            var note = new Note
            {
                Active = true,
                Time = i,
                Lane = 0,
                Id = i,
                Type = NoteType.Tap,
            };
            noteList.Add(note);
        }   
    }

    /// <summary>
    /// レーンに対して操作を適用.
    /// </summary>
    /// <param name="laneIndex"></param>
    private void ApplyLane(int laneIndex, List<Note> notes)
    {
        // そのレーンの生きてるノーツだけ取得.
        var note = notes.FirstOrDefault(note => note.Lane == laneIndex && note.Active);
        
        if (note == null)
        {
            // 生きてるノーツがない.
            return;
        }

        if (!CheckInRangeTime(note))
        {
            // 判定時間外.
            return;
        }
        
        // たたく.
        var rank = CheckRank(note);
        note.Active = false;
        onApplyRank.Value = (note, rank);
    }

    /// <summary>
    /// そのノーツが判定できる時間内か？.
    /// </summary>
    /// <param name="note"></param>
    /// <returns></returns>
    private bool CheckInRangeTime(Note note)
    {
        var noteTime = startTime + note.Time;
        var minTime = noteTime + -GameDefine.TimingGood;
        var maxTime = noteTime + GameDefine.TimingGood;
        if (minTime <= OnProgressTime.Value && OnProgressTime.Value <= maxTime)
        {
            // 範囲内.
            return true;
        }

        return false;
    }

    /// <summary>
    /// 判定ランク.
    /// </summary>
    /// <param name="note"></param>
    /// <returns></returns>
    private GameDefine.JudgeRank CheckRank(Note note)
    {
        var sub = Time.time - (startTime + note.Time);
        sub = Mathf.Abs(sub);

        switch (sub)
        {
            // Perfect.
            case <= GameDefine.TimingPerfect:
                return GameDefine.JudgeRank.Perfect;
            // Great.
            case <= GameDefine.TimingGreat:
                return GameDefine.JudgeRank.Great;
            // Good.
            case <= GameDefine.TimingGood:
                return GameDefine.JudgeRank.Good;
            default:
                // ここは来ないはず.
                Debug.LogError($"[GameModel] Invalid rank. Note:{note.Id}, Sub:{sub}");
                return GameDefine.JudgeRank.Miss;
        }
    }

    /// <summary>
    /// ノーツが通り過ぎたか.
    /// </summary>
    private Note CheckPassNote(float progressTime)
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

    public void Dispose()
    {
        disposable.Dispose();
    }
}
