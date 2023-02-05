using System.Collections.Generic;
using UniRx;

/// <summary>
/// 入力をコマンドに変換.
/// 入力をゲーム内の操作に抽象化する.
/// 他の操作と整合性も取りたいため、ここで全ての処理を詰め込む.
/// </summary>
public class InputCommand
{
    /// <summary>
    /// タップ.
    /// </summary>
    public IReadOnlyReactiveProperty<InputCommandData> Tap => tap;
    private readonly ReactiveProperty<InputCommandData> tap = new();
    /// <summary>
    /// ホールド.
    /// </summary>
    public IReadOnlyReactiveProperty<InputCommandData> Hold => hold;
    private readonly ReactiveProperty<InputCommandData> hold = new();


    private readonly Dictionary<int, InputEventData> inputEventMap = new();
    private readonly CompositeDisposable disposable = new();
    
    public InputCommand(IInputEvent inputEvent)
    {
        inputEvent.Push.SkipLatestValueOnSubscribe().Subscribe(inputEventData =>
        {
            // 触った.
            if (inputEventMap.ContainsKey(inputEventData.PointerId))
            {
                return;
            }
            
            inputEventMap.Add(inputEventData.PointerId, inputEventData);
            
            var data = new InputCommandData()
            {
                Lane = inputEventData.PointerId
            };
            tap.SetValueAndForceNotify(data);
        }).AddTo(disposable);

        // 離した.
        inputEvent.Release.SkipLatestValueOnSubscribe().Subscribe(inputEventData =>
        {
            if (inputEventMap.ContainsKey(inputEventData.PointerId))
            {
                inputEventMap.Remove(inputEventData.PointerId);    
            }
        }).AddTo(disposable);
    }
}
