using System.Collections.Generic;
using UniRx;
using UnityEngine;
using VContainer;

namespace Otoge.Domain
{
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
        public IReadOnlyReactiveProperty<InputCommandData> Release => _release;
        private readonly ReactiveProperty<InputCommandData> _release = new();

        // タップID, イベント紐づけ.
        private readonly Dictionary<int, InputEventData> inputEventMap = new();

        [Inject]
        public InputCommand(IInputEvent inputEvent, LifeCycle lifeCycle)
        {
            inputEvent.Push.SkipLatestValueOnSubscribe().Subscribe(inputEventData =>
            {
                // すでに触った.
                if (inputEventMap.ContainsKey(inputEventData.PointerId))
                {
                    // TODO:ホールドノーツ考慮.
                    // var holdData = new InputCommandData()
                    // {
                    //     Lane = inputEventData.PointerId
                    // };
                    // hold.SetValueAndForceNotify(holdData);
                    return;
                }

                inputEventMap.Add(inputEventData.PointerId, inputEventData);

                // タップ.
                var tapData = new InputCommandData()
                {
                    Lane = inputEventData.PointerId
                };
                tap.SetValueAndForceNotify(tapData);
            }).AddTo(lifeCycle.CompositeDisposable);

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
                    _release.SetValueAndForceNotify(holdReleaseData);
                }
            }).AddTo(lifeCycle.CompositeDisposable);
            
            Debug.Log("[InputCommand] Initialized.");
        }
    }
}