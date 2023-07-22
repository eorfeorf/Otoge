using System;
using Otoge.Domain;
using UnityEngine;

namespace Otoge.Presentation
{
    /// <summary>
    /// NoteViewの生成を行う.
    /// </summary>
    public class NoteViewFactory : MonoBehaviour
    {
        [SerializeField] 
        private Transform parent;
        
        [Header("Note prefab")]
        [SerializeField]
        private NoteViewTap tapNotePrefab;

        public NoteViewTap CreateNoteView<T>(Note note)
        {
            switch (note.Type)
            {
                case NoteType.Tap:
                    var view = Instantiate(tapNotePrefab, parent);
                    view.Initialize(note.Time, note.Lane);
                    return view; 
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
    }
}
