using UniRx;
using UnityEngine;

public class GamePresenter : MonoBehaviour
{
    [SerializeField] private GameView view;
    
    private GameModel model;

    private readonly CompositeDisposable disposable = new();

    private void Awake()
    {
        model = new GameModel(notes => view.Initialize(notes), disposable);
    }

    private void Start()
    {
        // 経過時間更新.
        model.OnProgressTime.Subscribe(progressTime =>
        {
            view.UpdateProgressTime(progressTime);
        }).AddTo(this);
        
        // ノーツが通り過ぎた.
        model.OnPassNote.Subscribe(note =>
        {
            view.PassNote(note);
        }).AddTo(this);
        
        // ノーツランク反映.
        model.OnApplyRank.SkipLatestValueOnSubscribe().Subscribe(value =>
        {
            view.ApplyRank(value.Item1, value.Item2);
        }).AddTo(this);
        
        // コンボ数変化.
        model.OnChangedCombo.SkipLatestValueOnSubscribe().Subscribe(count =>
        {
            view.ChangedCombo(count);
            
        }).AddTo(this);
        
        // リセット
        model.OnReset.SkipLatestValueOnSubscribe().Subscribe(_ =>
        {
            view.Reset();
        }).AddTo(this);
        
        model.Play();
    }

    private void OnDestroy()
    {
        disposable.Dispose();
        model = null;
    }
}
