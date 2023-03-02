using UnityEngine;

/// <summary>
/// スコア計算.
/// </summary>
public class ScoreCalculator
{
    /// <summary>
    /// 最終的なスコア計算.
    /// </summary>
    /// <returns></returns>
    public int Calc(ApplyNoteData data)
    {
        var score = GameDefine.JudgeRankScore[(int) data.Rank];
        return score;
    }
}
