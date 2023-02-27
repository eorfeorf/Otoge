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
    /// <summary>
    /// リリース.
    /// </summary>
    public IReadOnlyReactiveProperty<InputCommandData> HoldRelease => holdRelease;
    private readonly ReactiveProperty<InputCommandData> holdRelease = new();

    private readonly CompositeDisposable disposable = new();
    
    // タップID, イベント紐づけ.
    private readonly Dictionary<int, InputEventData> inputEventMap = new();
    
    public InputCommand(IInputEvent inputEvent)
    {
        inputEvent.Push.SkipLatestValueOnSubscribe().Subscribe(inputEventData =>
        {
            // すでに触った.
            if (inputEventMap.ContainsKey(inputEventData.PointerId))
            {
                // ホールド判定にするか？
                // 初回からホールド判定のほうがいい気もする？.
                // ホールドの開始ノーツは通常タップ処理で取るのもアリ.
                var holdData = new InputCommandData()
                {
                    Lane = inputEventData.PointerId
                };
                hold.SetValueAndForceNotify(holdData);
                return;
            }
            
            inputEventMap.Add(inputEventData.PointerId, inputEventData);
            
            // タップ.
            var tapData = new InputCommandData()
            {
                Lane = inputEventData.PointerId
            };
            tap.SetValueAndForceNotify(tapData);
        }).AddTo(disposable);

        // 離した.
        inputEvent.Release.SkipLatestValueOnSubscribe().Subscribe(inputEventData =>
        {
            if (inputEventMap.ContainsKey(inputEventData.PointerId))
            {
                inputEventMap.Remove(inputEventData.PointerId);
                
                // ホールドノーツの指を離した.
                // どうやってホールドノーツとわかるのか？.
                // 事前に判定するノーツの種類を知ってないとHoldReleaseは呼べないのでは？.
                // もしフリックがあった場合に指を離したのがフリックではないと言えるのか？.
                var holdReleaseData = new InputCommandData()
                {
                    Lane = inputEventData.PointerId
                };
                holdRelease.SetValueAndForceNotify(holdReleaseData);
            }
        }).AddTo(disposable);
    }
}
