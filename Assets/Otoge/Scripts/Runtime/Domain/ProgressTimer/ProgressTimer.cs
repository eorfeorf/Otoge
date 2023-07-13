using UniRx;
using UnityEngine;

namespace Otoge.Domain
{
    public class ProgressTimer
    {
        /// <summary>
        /// 経過時間更新.
        /// </summary>
        public IReadOnlyReactiveProperty<float> OnProgress => onProgress;

        private readonly ReactiveProperty<float> onProgress = new();

        private bool isStart = false;
        private float startTime = 0;

        public ProgressTimer(CompositeDisposable disposable)
        {
            Observable.EveryUpdate().Subscribe(_ =>
            {
                if (!isStart)
                {
                    return;
                }

                var progressTime = Time.time - startTime;
                onProgress.Value = progressTime;
            }).AddTo(disposable);
        }

        public void Start()
        {
            isStart = true;
            startTime = Time.time;
        }
    }
}