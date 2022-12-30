using System;
using UniRx;

public interface IInputEvent : IDisposable
{
    /// <summary>
    /// レーンID
    /// </summary>
    public IReadOnlyReactiveProperty<int> Touch { get; }
}