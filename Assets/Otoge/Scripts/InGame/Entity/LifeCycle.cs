using System;
using UniRx;

namespace Otoge.Scripts.InGame.Domain
{
    public class LifeCycle : IDisposable
    {
        public CompositeDisposable CompositeDisposable { get; private set; } = new CompositeDisposable();

        public void Dispose()
        {
            CompositeDisposable?.Dispose();
        }
    }
}