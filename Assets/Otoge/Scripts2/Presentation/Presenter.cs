using Otoge.Scripts2.Domain;
using Otoge.Scripts2.Domain.Entities;
using Otoge.Scripts2.Domain.UseCase;
using Otoge.Scripts2.Presentation.Note;
using UnityEngine;

namespace Otoge.Scripts2.Presentation
{
    public class Presenter : IPresenter
    {
        private readonly ViewModel _viewModel;
        private readonly NotesUseCase _notesUseCase;

        public Presenter(NotesUseCase notesUseCase)
        {
            var viewData = new InitializationViewData(notesUseCase.Notes, GameDefine.LANE_NUM, GameDefine.BPM, GameDefine.NOTE_SIZE);
            var view = GameObject.FindFirstObjectByType<View>();
            _viewModel = new ViewModel(viewData, view);
            _notesUseCase = notesUseCase;
        }

        public void DrawNotes(float progressTime)
        {
            
        }
    }
}