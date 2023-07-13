using UniRx;
using UnityEngine;

namespace Otoge.Domain
{
    public class InGameMainLoop
    {
        /// <summary>
        /// ゲームが開始されたか
        /// </summary>
        private bool isStart = false;
        
        private readonly LifeCycle _lifeCycle;
        private readonly ProgressTimer _progressTimer;
        private readonly NoteContainer _noteContainer;

        public InGameMainLoop(NoteContainer noteContainer, InGamePlayer inGamePlayer, ProgressTimer progressTimer, Combo combo, Score score, ScoreCalculator scoreCalculator, LifeCycle lifeCycle)
        {
            _lifeCycle = lifeCycle;
            _progressTimer = progressTimer;
            _noteContainer = noteContainer;
        
#if UNITY_EDITOR
            // 更新.
            Observable.EveryUpdate().Subscribe(_ =>
            {
                if (Input.GetKeyDown(KeyCode.R))
                {
                    Reset();
                }
            }).AddTo(_lifeCycle.CompositeDisposable);
#endif
        
            // 初期化完了通知.
            Debug.Log("[GameModel] Initialized.");
        }
        
        /// <summary>
        /// ゲーム開始.
        /// </summary>
        public void Play()
        {
            _progressTimer.Start();
            isStart = true;
        }

        /// <summary>
        /// 開始前にリセット.
        /// </summary>
        public void Reset()
        {
            _progressTimer.Start();
            _noteContainer.SetActiveAll(true);
        }
        
        ~InGameMainLoop()
        {
            _lifeCycle.Dispose();
        }
    }
}