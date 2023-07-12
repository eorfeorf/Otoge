using System.Collections.Generic;

namespace Otoge.Scripts.InGame.Application
{
    /// <summary>
    /// ノーツのデータを操作する.
    /// </summary>
    public class NoteController
    {
        private IList<global::Note> notes;
        
        public NoteController(IList<global::Note> notes)
        {
            this.notes = notes;
        }
        
        /// <summary>
        /// アクティブを設定.
        /// </summary>
        /// <param name="note"></param>
        /// <param name="isActive"></param>
        public void SetActive(global::Note note, bool isActive)
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