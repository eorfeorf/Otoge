using System.Collections.Generic;

public class NoteContainer
{
    public IList<Note> Notes => notes;
    private List<Note> notes = new();
    
    /// <summary>
    /// ノーツデータの生成.
    /// </summary>
    public NoteContainer()
    {
        for (int i = 0; i < 10; ++i)
        {
            var note = new Note
            {
                Active = true,
                Time = i,
                Lane = 0,
                Id = i,
                Type = NoteType.Tap,
            };
            notes.Add(note);
        }   
    }

    public void AllActive()
    {
        foreach(var note in notes)
        {
            note.Active = true;
        }
    }
}