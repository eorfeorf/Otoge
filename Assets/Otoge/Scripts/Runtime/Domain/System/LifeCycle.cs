using System;
using UniRx;

namespace Otoge.Domain
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