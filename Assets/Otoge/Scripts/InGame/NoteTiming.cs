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
        var early = note.Time + -GameDefine.TimingGood;
        var late = note.Time + GameDefine.TimingGood;
        if (early <= progressTime && progressTime <= late)
        {
            // 範囲内.
            return true;
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
                Debug.LogError($"[GameModel] Invalid rank. Note:{note.Id},{note.Time}, Sub:{sub}");
                return GameDefine.JudgeRank.Miss;
        }
    }
}