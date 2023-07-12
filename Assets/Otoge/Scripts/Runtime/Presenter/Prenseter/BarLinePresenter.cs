using System.Collections.Generic;
using Otoge.Scripts.InGame.Domain;
using Otoge.Scripts.InGame.Presentation.View;
using UniRx;
using UnityEngine;
using Vector3 = UnityEngine.Vector3;

namespace Otoge.Scripts.InGame.Presentation
{
    public class BarLinePresenter
    {
        /// <summary>
        /// 小節線
        /// </summary>
        private readonly List<BarView> barViews = new();

        private ProgressTimer _progressTimer;
        private CompositeDisposable _compositeDisposable = new CompositeDisposable();
        private BarLine _barLine;
        
        public BarLinePresenter(LifeCycle lifeCycle, BarView.Factory factory, Transform parent, BarLine barLine, ProgressTimer progressTimer)
        {
            _barLine = barLine;
            
            // View生成.
            var oneBeatTime = GameDefine.SEC60 / _barLine.Bpm;
            var oneBarTime = oneBeatTime * GameDefine.BEAT_PER_BAR;
            var count = (int)(barLine.BgmTime / oneBarTime) + 1;
            
            for (int i = 0; i < count; ++i)
            {
                var view = factory.Create(parent);
                barViews.Add(view);
            }

            // 更新.
            progressTimer.OnProgress.Subscribe(progressTime =>
            {
                int index = 0;
                foreach (var view in barViews)
                {
                    Update(view, ++index, progressTime);
                }
            }).AddTo(_compositeDisposable);
        }

        private void Update(BarView view, int index, float progressTime)
        {
            // BPMからバーの時間を計算.
            var oneBeatTime = GameDefine.SEC60 / _barLine.Bpm;
            var oneBarTime = oneBeatTime * GameDefine.BEAT_PER_BAR;
            var time = oneBarTime * index;

            var sub = time - progressTime;
            var posY = sub * GameDefine.NOTE_BASE_SPEED;
            
            var pos = view.transform.position;
            pos = new Vector3(pos.x, posY, pos.z);
            view.transform.position = pos;
        }
        
    }
}