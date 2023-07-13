using System.Collections.Generic;

namespace Otoge.Domain
{
    /// <summary>
    /// ノーツのデータを操作する.
    /// </summary>
    public class NoteController
    {
        private IList<Note> notes;
        
        public NoteController(IList<Note> notes)
        {
            this.notes = notes;
        }
        
        /// <summary>
        /// アクティブを設定.
        /// </summary>
        /// <param name="note"></param>
        /// <param name="isActive"></param>
        public void SetActive(Note note, bool isActive)
        {
            note.Active = isActive;
        }

        /// <summary>
        /// アクティブを設定.
        /// </summary>
        /// <param name="note"></param>
        public void SetActiveAll(bool isActive)
        {
            foreach (var note in notes)
            {
                note.Active = isActive;
            }
        }
    }
}