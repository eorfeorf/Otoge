using System.Collections.Generic;

namespace Otoge.Domain
{
    public interface INoteRepository
    {
        public IList<Note> Notes { get; }
    }
}