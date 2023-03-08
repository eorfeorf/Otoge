using UnityEngine;

/// <summary>
/// 判定周り
/// </summary>
public static class NoteTiming
{
    /// <summary>
    /// そのノーツが判定できる時間内か？.
    /// </summary>
    /// <param name="noteData"></param>
    /// <returns></returns>
    public static bool CheckInRangeApplyTime(NoteType type, float noteTime, float progressTime)
    {
        switch (type)
        {
            case NoteType.Tap:
            {
                var time = noteTime;
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
                var time = noteTime;
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
                var time = noteTime;
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
    /// <param name="noteData"></param>
    /// <returns></returns>
    public static GameDefine.JudgeRank CheckRank(float noteTime, float progressTime)
    {
        var sub = noteTime - progressTime;
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
                return GameDefine.JudgeRank.Miss;
        }
    }
}