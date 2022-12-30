using UniRx;
using UnityEngine;

public class GamePresenter : MonoBehaviour
{
    [SerializeField] private GameView view;
    
    private InputEventFactory inputEventFactory;
    private GameModel model;

    private void Awake()
    {
    }

    private void Start()
    {
        inputEventFactory = new InputEventFactory();
        model = new GameModel(inputEventFactory.InputEvent,
            notes => view.Initialize(notes));
        
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
        
        model.Play();
    }
}
