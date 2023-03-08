using System.Collections.Generic;

namespace Otoge.Scripts.InGame.Application
{
    /// <summary>
    /// ノーツのデータを操作する.
    /// </summary>
    public class NoteController
    {
        private IList<NoteData> notes;
        
        public NoteController(IList<NoteData> notes)
        {
            this.notes = notes;
        }
        
        /// <summary>
        /// アクティブを設定.
        /// </summary>
        /// <param name="noteData"></param>
        /// <param name="isActive"></param>
        public void SetActive(NoteData noteData, bool isActive)
        {
            noteData.Active = isActive;
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