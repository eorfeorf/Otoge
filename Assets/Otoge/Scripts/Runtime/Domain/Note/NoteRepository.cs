using System.Collections.Generic;

namespace Otoge.Domain
{
    public class NoteRepository : INoteRepository
    {
        /// <summary>
        /// ノーツ.
        /// </summary>
        public IList<Note> Notes { get; private set; }

        public NoteRepository(List<Note> notes)
        {
            Notes = notes;
        }
    }
}