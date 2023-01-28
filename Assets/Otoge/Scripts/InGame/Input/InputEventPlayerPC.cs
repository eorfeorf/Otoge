using UniRx;
using UnityEngine;

/// <summary>
/// PCでの入力.
/// </summary>
public class InputEventPlayerPC : IInputEvent
{
    /// <summary>
    /// 押した.
    /// </summary>
    public IReadOnlyReactiveProperty<InputEventData> Push => push;
    private ReactiveProperty<InputEventData> push = new();
    
    /// <summary>
    /// 離した.
    /// </summary>
    public IReadOnlyReactiveProperty<InputEventData> Release => release;
    private ReactiveProperty<InputEventData> release = new();

    public InputEventPlayerPC(CompositeDisposable disposable)
    {
        Observable.EveryUpdate().Subscribe(_ =>
        {
            var data = new InputEventData();
            
            data.PointerId = 0;
            if (Input.GetKey(KeyCode.F))
            {
                push.SetValueAndForceNotify(data);
            }
            else
            {
                release.SetValueAndForceNotify(data);
            }
            
            data.PointerId = 1;
            if (Input.GetKey(KeyCode.J))
            {
                push.SetValueAndForceNotify(data);
            }
            else
            {
                data.PointerId = 1;
                release.SetValueAndForceNotify(data);
            }
        }).AddTo(disposable);
    }
}