using System.Collections.Generic;
using Otoge.Domain;
using UnityEngine;
using VContainer;

namespace Otoge.Presentation
{
    /// <summary>
    /// NoteViewの実体を管理するクラス.
    /// </summary>
    public class NoteViewRepository
    {
        public IList<NoteViewTap> NoteView => _noteView;
        private readonly List<NoteViewTap> _noteView = new();

        [Inject]
        public NoteViewRepository(NoteViewFactory noteViewFactory, NoteContainer noteContainer)
        {
            foreach (var note in noteContainer.Notes)
            {
                var view = noteViewFactory.CreateNoteView<NoteViewTap>(note.Value);
                _noteView.Add(view);   
            }
            
            Debug.Log("[NoteViewRepository] Initialized");
        }
    }
}