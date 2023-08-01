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
        public IList<NoteViewTap> NoteViews => _noteViews;
        private readonly List<NoteViewTap> _noteViews = new();

        [Inject]
        public NoteViewRepository(NoteViewFactory noteViewFactory, NoteContainer noteContainer, InGameViewInfo inGameViewInfo)
        {
            foreach (var note in noteContainer.Notes.Select(x => x.Value))
            {
                var view = noteViewFactory.CreateNoteView(note.Type);
                view.Initialize(note.Time, note.ExData.Lane, note.Size, inGameViewInfo);
                _noteViews.Add(view);
            }
            
            Debug.Log("[NoteViewRepository] Initialized");
        }

        public void Dispose()
        {
            _noteViews.Clear();
        }
    }
}