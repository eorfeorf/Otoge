using System.Collections.Generic;
using System.Linq;

/// <summary>
/// 読み込んだノーツの管理.
/// </summary>
public class NoteContainer
{
    public IList<Note> Notes => notes;
    private List<Note> notes = new();
    
    /// <summary>
    /// ノーツデータの生成.
    /// TODO:譜面からデータを変換.
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

    /// <summary>
    /// 全てのノーツを有効にする.
    /// </summary>
    public void AllActive()
    {
        foreach(var note in notes)
        {
            note.Active = true;
        }
    }

    /// <summary>
    /// あるノーツのアクティブを設定.
    /// </summary>
    /// <param name="isActive"></param>
    public void SetNoteActive(Note note, bool isActive)
    {
        foreach (var n in notes)
        {
            if (n.Id != note.Id) continue;
            note.Active = isActive;
            break;
        }
    }
}