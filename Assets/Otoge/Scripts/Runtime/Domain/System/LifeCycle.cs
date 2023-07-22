using System;
using UniRx;
using UnityEngine;

namespace Otoge.Domain
{
    public class LifeCycle : IDisposable
    {
        public CompositeDisposable CompositeDisposable { get; private set; } = new CompositeDisposable();

        public LifeCycle()
        {
            Debug.Log("[LifeCycle] Initialized.");
        }
        
        public void Dispose()
        {
            CompositeDisposable?.Dispose();
        }
    }
}