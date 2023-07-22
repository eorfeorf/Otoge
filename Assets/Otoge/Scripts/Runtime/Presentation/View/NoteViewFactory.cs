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

        public NoteViewTap CreateNoteView(NoteType type)
        {
            switch (type)
            {
                case NoteType.Tap:
                    return Instantiate(tapNotePrefab, parent);
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
    }
}
