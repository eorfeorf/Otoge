using UnityEngine;

/// <summary>
/// 判定周り
/// </summary>
public static class NoteTiming
{
    /// <summary>
    /// そのノーツが判定できる時間内か？.
    /// </summary>
    /// <param name="note"></param>
    /// <returns></returns>
    public static bool CheckInRangeApplyTime(Note note, float progressTime)
    {
        switch (note.Type)
        {
            case NoteType.Tap:
            {
                var time = note.Time;
                var early = time + -GameDefine.TimingGood;
                var late = time + GameDefine.TimingGood;
                if (early <= progressTime && progressTime <= late)
                {
                    // 範囲内.
                    return true;
                }

                break;
            }
            case NoteType.HoldStart:
            {
                // ノーツがアクティブかどうか気にする必要がありそう.
                // NoteじゃなくてNoteBaseでもらった方がよいのでは.
                var time = note.Time;
                var early = time + -GameDefine.TimingGood;
                var late = time + GameDefine.TimingGood;
                if (early <= progressTime && progressTime <= late)
                {
                    // 範囲内.
                    return true;
                }

                break;
            }

            case NoteType.HoldEnd:
            {
                // ノーツがアクティブかどうか気にする必要がありそう.
                // NoteじゃなくてNoteBaseでもらった方がよいのでは.
                var time = note.Time;
                var early = time + -GameDefine.TimingGood;
                var late = time + GameDefine.TimingGood;
                if (early <= progressTime && progressTime <= late)
                {
                    // 範囲内.
                    return true;
                }

                break;
            }
                break;
            default:
                break;
        }

        return false;
    }


    /// <summary>
    /// 判定ランク.
    /// </summary>
    /// <param name="note"></param>
    /// <returns></returns>
    public static GameDefine.JudgeRank CheckRank(Note note, float progressTime)
    {
        var sub = note.Time - progressTime;
        sub = Mathf.Abs(sub);

        switch (sub)
        {
            case <= GameDefine.TimingPerfect:
                return GameDefine.JudgeRank.Perfect;
            case <= GameDefine.TimingGreat:
                return GameDefine.JudgeRank.Great;
            case <= GameDefine.TimingGood:
                return GameDefine.JudgeRank.Good;
            default:
                // ここは来ないはず.
                Debug.LogError($"[GameModel] Invalid rank. Note:{note.PairId},{note.Time}, Sub:{sub}");
                return GameDefine.JudgeRank.Miss;
        }
    }
}