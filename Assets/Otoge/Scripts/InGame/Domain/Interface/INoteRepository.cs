using System.Collections.Generic;

namespace Otoge.Scripts.InGame.Application.Interface
{
    public interface INoteRepository
    {
        public IList<NoteData> Notes { get; }
    }
}