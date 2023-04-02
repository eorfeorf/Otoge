using System.Collections.Generic;
using Otoge.Scripts2.Domain.Note;

namespace Otoge.Scripts2.Domain.UseCase
{
    public class NotesUseCase
    {
        /// <summary>
        /// ノーツデータ.
        /// </summary>
        public IDictionary<int, Entities.Note> Notes => _notesRepository.Notes;

        private readonly INotesRepository _notesRepository;
        private readonly IPresenter _presenter;

        public NotesUseCase(INotesRepository notesRepository, IPresenter presenter)
        {
            _notesRepository = notesRepository;
            _presenter = presenter;
        }
        
        /// <summary>
        /// ノーツ読み込み
        /// </summary>
        /// <returns>true:成功 false:失敗</returns>
        private bool Load(int musicalScoreId)
        {
            _notesRepository.Load(musicalScoreId);
                
            return true;
        }
    }
}