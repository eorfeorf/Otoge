using System.Collections.Generic;
using Otoge.Domain;
using UniRx;
using UnityEngine;
using VContainer;

namespace Otoge.Presentation
{
    /// <summary>
    /// ノーツのPresenter.
    /// DomainのデータからViewを反映させる.
    /// </summary>
    public class NotePresenter
    {
        /// <summary>
        /// UID,noteView
        /// </summary>
        private readonly Dictionary<int, NoteView> _noteViews = new();

        private ProgressTimer _progressTimer;
        private CompositeDisposable _compositeDisposable = new CompositeDisposable();
        
        [Inject]
        public NotePresenter(ProgressTimer progressTimer)
        {
            _progressTimer = progressTimer;
            
            _progressTimer.OnProgress.Subscribe(progressTime =>
            {
                UpdateProgressTime(progressTime);
            }).AddTo(_compositeDisposable);
        }
        
        /// <summary>
        /// 経過時間更新.
        /// </summary>
        /// <param name="progressTime"></param>
        private void UpdateProgressTime(float progressTime)
        {
            // ノーツの位置.
            foreach (var view in _noteViews)
            {
                // ノーツ時間と経過時間を比較してノーツの位置を計算.
                var noteView = view.Value;
                var pos = noteView.Transform.position;
                var sub = view.Value.Time - progressTime;
                var posY = sub * GameDefine.NOTE_BASE_SPEED;
                pos = new Vector3(pos.x, posY, pos.z);
                view.Value.Transform.position = pos;
            }
        }

        /// <summary>
        /// ノーツ適用.
        /// </summary>
        /// <param name="note"></param>
        public void ApplyNote(Note note)
        {
            _noteViews[note.UId].GameObject.SetActive(false);
        }

        /// <summary>
        /// ノーツが通り過ぎた.
        /// 判定範囲外になったタイミングで呼び出される.
        /// </summary>
        /// <param name="note"></param>
        public void PassNote(Note note)
        {
            _noteViews[note.UId].GameObject.SetActive(false);
        }

        /// <summary>
        /// リセット.
        /// </summary>
        public void Reset()
        {
            foreach (var view in _noteViews)
            {
                view.Value.GameObject.SetActive(true);
            }
        }
    }
}