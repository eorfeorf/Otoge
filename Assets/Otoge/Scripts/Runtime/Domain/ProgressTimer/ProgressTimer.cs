using UniRx;
using UnityEngine;
using VContainer;

namespace Otoge.Domain
{
    public class ProgressTimer
    {
        /// <summary>
        /// 経過時間更新.
        /// </summary>
        public IReadOnlyReactiveProperty<float> OnProgress => _onProgress;
        private readonly ReactiveProperty<float> _onProgress = new();
        
        private bool _isStart = false;
        private float _startTime = 0;

        [Inject]
        public ProgressTimer(LifeCycle lifeCycle)
        {
            // TODO:音のシークにする.
            Observable.EveryUpdate().Subscribe(_ =>
            {
                if (!_isStart)
                {
                    return;
                }

                var progressTime = Time.time - _startTime;
                _onProgress.Value = progressTime;
            }).AddTo(lifeCycle.CompositeDisposable);
            
            Debug.Log("[ProgressTimer] Initialized.");
        }

        public void Start()
        {
            _isStart = true;
            _startTime = Time.time;
        }
    }
}