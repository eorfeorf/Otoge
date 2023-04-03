using Otoge.Scripts2.Domain;
using Otoge.Scripts2.Domain.Entities;
using Otoge.Scripts2.Domain.UseCase;
using Otoge.Scripts2.Presentation;
using Otoge.Scripts2.Presentation.Note;
using UnityEngine;

namespace Otoge.Scripts2.Adapter.Presenters
{
    public class Presenter : IPresenter
    {
        private readonly NotesUseCase _notesUseCase;

        public Presenter(NotesUseCase notesUseCase)
        {
            var viewData = new InitializationViewData(notesUseCase.Notes, GameDefine.LANE_NUM, GameDefine.BPM, GameDefine.NOTE_SIZE);
            _notesUseCase = notesUseCase;
        }

        public void DrawNotes(float progressTime)
        {
            
        }
    }
}