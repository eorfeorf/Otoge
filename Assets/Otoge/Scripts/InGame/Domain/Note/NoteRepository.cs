using System.Collections.Generic;
using Otoge.Scripts.InGame.Application.Interface;

public class NoteRepository : INoteRepository
{
    /// <summary>
    /// ノーツ.
    /// </summary>
    public IList<NoteData> Notes { get; private set; }

    public NoteRepository(List<NoteData> notes)
    {
        Notes = notes;
    }
}
