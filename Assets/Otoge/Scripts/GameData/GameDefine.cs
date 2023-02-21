
using UnityEngine;

/// <summary>
/// 定数.
/// </summary>
public static class GameDefine
{
    // レーン数.
    public static readonly int LaneNum = 2;
    
    // 判定ランク.
    public enum JudgeRank
    {
        Perfect, Great, Good, Miss
    }
    // 判定用フレームレート
    private const float TimingRate = 60f; // fps
    // タイミングは+方向-方向どちらも
    public const float TimingPerfect = 1f / TimingRate;
    public const float TimingGreat = 3f / TimingRate;
    public const float TimingGood = 5f / TimingRate;
}