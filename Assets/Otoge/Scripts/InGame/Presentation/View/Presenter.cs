using Otoge.Scripts.InGame.Application.Interface;
using UnityEngine;

namespace Otoge.Scripts.InGame.Presentation
{
    public class Presenter : IPresentation
    {
        /// <summary>
        /// 経過時間更新.
        /// </summary>
        /// <param name="progressTime"></param>
        public void UpdateProgressTime(float progressTime)
        {
            // ノーツの位置.
            foreach (var view in noteViews)
            {
                // ノーツ時間と経過時間を比較してノーツの位置を計算.
                var noteView = view.Value;
                var pos = noteView.Transform.position;
                var sub = view.Value.Time - progressTime;
                var posY = sub * GameDefine.NOTE_BASE_SPEED;
                pos = new Vector3(pos.x, posY, pos.z);
                view.Value.Transform.position = pos;
            }

            // 小節線.
            foreach (var view in barViews)
            {
                var pos = view.Transform.position;
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
            noteViews[note.UId].GameObject.SetActive(false);
            effectView.Play(note.Lane);
        }

        /// <summary>
        /// ノーツが通り過ぎた.
        /// 判定範囲外になったタイミングで呼び出される.
        /// </summary>
        /// <param name="note"></param>
        public void PassNote(Note note)
        {
            noteViews[note.UId].GameObject.SetActive(false);
        }

        /// <summary>
        /// リセット.
        /// </summary>
        public void Reset()
        {
            foreach (var view in noteViews)
            {
                view.Value.GameObject.SetActive(true);
            }
        }
    }
}