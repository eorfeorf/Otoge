using UniRx;
using UnityEngine;

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

        public ProgressTimer(CompositeDisposable disposable)
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
            }).AddTo(disposable);
        }

        public void Start()
        {
            _isStart = true;
            _startTime = Time.time;
        }
    }
}