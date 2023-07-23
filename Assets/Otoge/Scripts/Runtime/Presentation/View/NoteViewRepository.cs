using System;
using System.Collections.Generic;
using System.Linq;
using Otoge.Domain;
using UnityEngine;
using VContainer;

namespace Otoge.Presentation
{
    /// <summary>
    /// NoteViewの実体を管理するクラス.
    /// </summary>
    public class NoteViewRepository : IDisposable
    {
        public IList<NoteViewTap> NoteView => _noteView;
        private readonly List<NoteViewTap> _noteView = new();

        [Inject]
        public NoteViewRepository(NoteViewFactory noteViewFactory, NoteContainer noteContainer, InGameViewInfo inGameViewInfo)
        {
            foreach (var note in noteContainer.Notes.Select(x => x.Value))
            {
                var view = noteViewFactory.CreateNoteView(note.Type);
                view.Initialize(note.Time, note.Lane, note.Size, inGameViewInfo);
                _noteView.Add(view);
            }
            
            Debug.Log("[NoteViewRepository] Initialized");
        }

        public void Dispose()
        {
            _noteView.Clear();
        }
    }
}