using Otoge.Scripts2.Domain.Input;
using UniRx;

namespace Otoge.Scripts2.Domain
{
    public interface IDevice
    {
        /// <summary>
        /// 触れた.
        /// </summary>
        public IReadOnlyReactiveProperty<InputEventData> Push { get; }
    
        /// <summary>
        /// 離した.
        /// </summary>
        public IReadOnlyReactiveProperty<InputEventData> Release { get; }
    }
}