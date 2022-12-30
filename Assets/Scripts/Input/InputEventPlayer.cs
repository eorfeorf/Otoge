using UniRx;
using UnityEngine;

public class InputEventPlayer : IInputEvent
{
    public IReadOnlyReactiveProperty<int> Touch => touch;
    private ReactiveProperty<int> touch = new();

    // public IReadOnlyReactiveProperty<Unit> Left => left;
    // private ReactiveProperty<Unit> left = new(Unit.Default);
    // public IReadOnlyReactiveProperty<Unit> Right => right;
    // private readonly ReactiveProperty<Unit> right = new(Unit.Default);

    private CompositeDisposable disposable = new();
    
    public InputEventPlayer()
    {
        Observable.EveryUpdate().Subscribe(_ =>
        {
            if (Input.GetKeyDown(KeyCode.F))
            {
                touch.SetValueAndForceNotify(0);
                //left.SetValueAndForceNotify(Unit.Default);
            }

            if (Input.GetKeyDown(KeyCode.J))
            {
                touch.SetValueAndForceNotify(1);
                //left.SetValueAndForceNotify(Unit.Default);
            }   
        }).AddTo(disposable);
    }

    public void Dispose()
    {
        disposable.Dispose();
    }

}