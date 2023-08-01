using Otoge.Domain;
using UniRx;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace Otoge.Presentation
{
    /// <summary>
    /// ノーツのPresenter.
    /// DomainのデータからViewを反映させる.
    /// </summary>
    public class NotePresenter : IInitializable
    {
        private CompositeDisposable _compositeDisposable = new CompositeDisposable();
        private readonly ProgressTimer _progressTimer;
        private readonly NoteViewRepository _noteViewRepository;

        public void Initialize()
        {
            
        }
        
        [Inject]
        public NotePresenter(NoteViewRepository noteViewRepository, ProgressTimer progressTimer,
            InGamePlayer inGamePlayer, LifeCycle lifeCycle)
        {
            _progressTimer = progressTimer;
            _noteViewRepository = noteViewRepository;

            // ノーツ更新.
            _progressTimer.OnProgress.Subscribe(progressTime =>
            {
                UpdateProgressTime(progressTime);
            }).AddTo(_compositeDisposable);
            
            // ノーツ適用.
            inGamePlayer.OnApplyNote.SkipLatestValueOnSubscribe().Subscribe(applyData =>
            {
                ApplyNote(applyData.Note);
            }).AddTo(_compositeDisposable);
            
            // ノーツ通過.
            inGamePlayer.OnPassNote.SkipLatestValueOnSubscribe().Subscribe(applyData =>
            {
                PassNote(applyData.Note);
            }).AddTo(_compositeDisposable);
            
            Debug.Log("[NotePresenter] Initialized.");
        }
        
        /// <summary>
        /// 経過時間更新.
        /// </summary>
        /// <param name="progressTime"></param>
        private void UpdateProgressTime(float progressTime)
        {
            // ノーツの位置.
            foreach (var view in _noteViewRepository.NoteViews)
            {
                // ノーツ時間と経過時間を比較してノーツの位置を計算.
                var noteView = view;
                var pos = noteView.Transform.position;
                var sub = view.Time - progressTime;
                var posY = sub * GameDefine.NOTE_BASE_SPEED;
                pos = new Vector3(pos.x, posY, pos.z);
                view.Transform.position = pos;
            }
        }

        /// <summary>
        /// ノーツ適用.
        /// </summary>
        /// <param name="noteBase"></param>
        public void ApplyNote(NoteBase noteBase)
        {
            _noteViewRepository.NoteViews[noteBase.Uid].GameObject.SetActive(false);
        }

        /// <summary>
        /// ノーツが通り過ぎた.
        /// 判定範囲外になったタイミングで呼び出される.
        /// </summary>
        /// <param name="noteBase"></param>
        public void PassNote(NoteBase noteBase)
        {
            _noteViewRepository.NoteViews[noteBase.Uid].GameObject.SetActive(false);
        }

        /// <summary>
        /// リセット.
        /// </summary>
        public void Reset()
        {
            foreach (var view in _noteViewRepository.NoteViews)
            {
                view.GameObject.SetActive(true);
            }
        }
    }
}