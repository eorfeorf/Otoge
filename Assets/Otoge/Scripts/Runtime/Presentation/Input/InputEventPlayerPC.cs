using Otoge.Domain;
using UniRx;
using UnityEngine;

namespace Otoge.Presentation
{
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
                foreach (var e in GameDefine.InputKeyToLane)
                {
                    var data = new InputEventData();
                    data.PointerId = e.LaneIndex;

                    if (Input.GetKey(e.Key))
                    {
                        push.SetValueAndForceNotify(data);
                    }
                    else
                    {
                        release.SetValueAndForceNotify(data);
                    }
                }
            }).AddTo(disposable);
        }
    }
}