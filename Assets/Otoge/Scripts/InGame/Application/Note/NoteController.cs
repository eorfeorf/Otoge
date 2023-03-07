namespace Otoge.Scripts.InGame.Application
{
    /// <summary>
    /// ノーツのデータを操作する.
    /// </summary>
    public class NoteController
    {
        public NoteController(NoteContainer noteContainer)
        {
            
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
                note.Value.Active = isActive;
            }
        }
    }
}