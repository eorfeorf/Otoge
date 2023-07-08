using System.Collections.Generic;

/// <summary>
/// 読み込んだノーツの管理.
/// </summary>
public class NoteDataContainer
{
    public IDictionary<int, NoteData> Notes => notes;
    private readonly Dictionary<int, NoteData> notes = new();
    
    /// <summary>
    /// ノーツデータの生成.
    /// TODO:譜面からデータに変換.
    /// </summary>
    public NoteDataContainer()
    {
        float startTime = 0f;
        var bpm = GameDefine.BPM;
        var spb = GameDefine.SEC60 / bpm; // secondsPerBeat
        for (int i = 0; i < GameDefine.NOTE_NUM; ++i)
        {
            // 譜面情報からわかる.
            var time = (i * spb) + startTime;
            var note = Create(NoteType.Tap, i, i % GameDefine.LANE_NUM, time, i);
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
    private NoteData Create(NoteType type, int pairId, int lane, float time, int uid)
    {
        var note = new NoteData
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
}