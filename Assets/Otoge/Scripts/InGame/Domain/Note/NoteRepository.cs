using System.Collections.Generic;
using Otoge.Scripts.InGame.Application.Interface;

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
