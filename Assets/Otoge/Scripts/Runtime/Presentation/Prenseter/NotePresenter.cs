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
        public NotePresenter(NoteViewRepository noteViewRepository, ProgressTimer progressTimer)
        {
            _progressTimer = progressTimer;
            _noteViewRepository = noteViewRepository;
            
            // ノーツ更新.
            _progressTimer.OnProgress.Subscribe(progressTime =>
            {
                UpdateProgressTime(progressTime);
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
            foreach (var view in _noteViewRepository.NoteView)
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
        /// <param name="note"></param>
        public void ApplyNote(Note note)
        {
            _noteViewRepository.NoteView[note.UId].GameObject.SetActive(false);
        }

        /// <summary>
        /// ノーツが通り過ぎた.
        /// 判定範囲外になったタイミングで呼び出される.
        /// </summary>
        /// <param name="note"></param>
        public void PassNote(Note note)
        {
            _noteViewRepository.NoteView[note.UId].GameObject.SetActive(false);
        }

        /// <summary>
        /// リセット.
        /// </summary>
        public void Reset()
        {
            foreach (var view in _noteViewRepository.NoteView)
            {
                view.GameObject.SetActive(true);
            }
        }
    }
}