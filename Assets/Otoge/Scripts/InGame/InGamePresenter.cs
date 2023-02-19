using UniRx;
using UnityEngine;

public class InGamePresenter : MonoBehaviour
{
    [SerializeField] private InGameView view;
    
    private InGameModel model;
    private readonly CompositeDisposable disposable = new();

    private void Awake()
    {
        model = new InGameModel(notes => view.Initialize(notes), disposable);
    }

    private void Start()
    {
        // 経過時間更新.
        model.OnProgressTime.Subscribe(progressTime =>
        {
            view.UpdateProgressTime(progressTime);
        }).AddTo(this);
        
        // ノーツが通り過ぎた.
        model.OnPassNote.SkipLatestValueOnSubscribe().Subscribe(note =>
        {
            view.PassNote(note);
        }).AddTo(this);
        
        // ノーツランク反映.
        model.OnApplyNote.SkipLatestValueOnSubscribe().Subscribe(data =>
        {
            view.ApplyNote(data.Note);
            view.RankView.ApplyRankText(data.Rank);
        }).AddTo(this);
        
        // コンボ数変化.
        model.OnChangedCombo.SkipLatestValueOnSubscribe().Subscribe(count =>
        {
            view.ComboView.ChangedCombo(count);
        }).AddTo(this);
        
        // リセット
        model.OnReset.SkipLatestValueOnSubscribe().Subscribe(_ =>
        {
            view.Reset();
        }).AddTo(this);
        
        // TODO：後でかえる.
        model.Play();
    }

    private void OnDestroy()
    {
        disposable.Dispose();
        model = null;
    }
}
