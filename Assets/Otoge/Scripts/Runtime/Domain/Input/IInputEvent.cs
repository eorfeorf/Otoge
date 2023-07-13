using UniRx;

namespace Otoge.Domain
{
    /// <summary>
    /// 入力操作の定義.
    /// </summary>
    public interface IInputEvent
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
