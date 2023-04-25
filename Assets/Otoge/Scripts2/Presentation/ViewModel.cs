using System.Collections.Generic;
using System.Linq;
using Otoge.Scripts2.Presentation.Note;
using Otoge.Scripts2.Presentation.Views;
using UnityEngine;

namespace Otoge.Scripts2.Presentation
{
    public class ViewModel
    {
        /// <summary>
        /// View.
        /// </summary>
        private View _view;
        
        /// <summary>
        /// UID,noteView
        /// </summary>
        private readonly Dictionary<int, NoteView> _noteViews = new();
        
        /// <summary>
        /// レーン位置.
        /// </summary>
        /// <returns></returns>
        private readonly List<Vector3> _lanePosition = new();
        
        /// <summary>
        /// 小節線
        /// </summary>
        private readonly List<BarView> _barViews = new();

        private float barTime; // 1小節当たりの時間

        public ViewModel(InitializationViewData data, View view)
        {
            _view = view;
            var notes = data.Notes;
            
            // ノーツ生成.
            foreach (var note in data.Notes.Select(x => x.Value))
            {
                // 必要な情報はNoteViewに詰め込む.
                var noteView = _view.Create<NoteView>(_view.NotesParent);
                noteView.Initialize(note.Time, note.Lane);
                _noteViews.Add(note.UId, noteView);
            }

            // レーン位置計算.
            for (int i = 0; i < data.MaxLaneNum; ++i)
            {
                var posX = i - data.MaxLaneNum / 2.0f + (data.NoteSize.x/2f);
                //var posX = i;
                _lanePosition.Add(new Vector3(posX, 0f, 0f));
                Debug.Log($"[InGameView] lane:{i}, posX:{posX}");
            }
            
            // ノーツのX位置.
            foreach (var noteView in _noteViews.Select(x => x.Value))
            {
                noteView.transform.position = _lanePosition[noteView.LaneIndex];
            }
        }

        public void UpdateNotes()
        {
            
        }
    }
}