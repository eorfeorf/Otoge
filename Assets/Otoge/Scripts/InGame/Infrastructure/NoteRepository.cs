using System.Collections.Generic;
using Otoge.Scripts.InGame.Application.Interface;

public class NoteRepository : INoteRepository
{
    /// <summary>
    /// ノーツ.
    /// </summary>
    public List<NoteData> Notes { get; private set; }

    public NoteRepository(List<NoteData> notes)
    {
        Notes = notes;
    }
}