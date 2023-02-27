using System.Collections.Generic;
using TreeEditor;
using UnityEngine;


/// <summary>
/// 読み込んだノーツの管理.
/// </summary>
public class NoteContainer
{
    public IDictionary<int, Note> Notes => notes;
    private readonly Dictionary<int, Note> notes = new();
    
    /// <summary>
    /// ノーツデータの生成.
    /// TODO:譜面からデータに変換.
    /// </summary>
    public NoteContainer()
    {
        for (int i = 0; i < 50; ++i)
        {
            // 譜面情報からわかる.
            var note = Create(NoteType.Tap, i, i % GameDefine.LANE_NUM, i, i);
            notes.Add(note.UId, note);
        }   
    }

    /// <summary>
    /// ノーツ生成.
    /// </summary>
    /// <param name="type"></param>
    /// <param name="pairId"></param>
    /// <param name="lane"></param>
    /// <param name="time"></param>
    /// <param name="active"></param>
    /// <returns></returns>
    private Note Create(NoteType type, int pairId, int lane, float time, int uid)
    {
        var note = new Note
        {
            Type = type,
            Active = true,
            Lane = lane,
            Time = time,
            PairId = pairId,
            UId = uid,
        };

        return note;
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