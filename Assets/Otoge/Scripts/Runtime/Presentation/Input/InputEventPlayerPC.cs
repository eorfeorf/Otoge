using Otoge.Domain;
using UniRx;
using UnityEngine;
using UnityEngine.InputSystem;
using VContainer;

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

        [Inject]
        public InputEventPlayerPC(LifeCycle lifeCycle)
        {
            Observable.EveryUpdate().Subscribe(_ =>
            {
                foreach (var e in InputConfigure.InputKeyToLane)
                {
                    var data = new InputEventData
                    {
                        TouchId = e.LaneIndex,
                        Time = Time.time,
                    };

                    Keyboard keyboard = Keyboard.current;
                    if (keyboard[e.Key].wasPressedThisFrame)
                    {
                        push.SetValueAndForceNotify(data);
                    }
                    else if(keyboard[e.Key].wasReleasedThisFrame)
                    {
                        release.SetValueAndForceNotify(data);
                    }
                }
            }).AddTo(lifeCycle.CompositeDisposable);
        }
    }
}