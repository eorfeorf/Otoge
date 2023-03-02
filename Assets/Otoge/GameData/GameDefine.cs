
using System;
using UnityEngine;

[Serializable]
public struct InputKeyToLaneData
{
    public KeyCode Key;
    public int LaneIndex;
}

/// <summary>
/// 定数.
/// </summary>
public static class GameDefine
{
    /// <summary>
    /// レーン数.
    /// </summary>
    public static int LANE_NUM => InputKeyToLane.Length;

    /// <summary>
    /// ノーツスピード.
    /// </summary>
    public static float NOTE_BASE_SPEED = 5f;

    /// <summary>
    /// 入力とレーンの割り当て.
    /// TODO:LaneIndexを動的に変更.
    /// </summary>
    public static readonly InputKeyToLaneData[] InputKeyToLane = {
        new()
        {
            Key = KeyCode.S,
            LaneIndex =  0,
        },
        new()
        {
            Key = KeyCode.D,
            LaneIndex =  1,
        },
        new()
        {
            Key = KeyCode.F,
            LaneIndex =  2,
        },
        new()
        {
            Key = KeyCode.J,
            LaneIndex =  3,
        },
        new()
        {
            Key = KeyCode.K,
            LaneIndex =  4,
        },
        new()
        {
            Key = KeyCode.L,
            LaneIndex =  5,
        },
    };

    // ノーツが通り過ぎて消えるまでの距離.
    public const float NOTE_VIEW_PASSED_DISABLE_TIME = 0.5f;

    /// <summary>
    /// 判定ランク.
    /// </summary>
    public enum JudgeRank
    {
        Perfect,
        Great,
        Good,
        Miss,
        Max,
    }

    /// <summary>
    /// ノーツ判定によるスコア.
    /// </summary>
    public static readonly int[] JudgeRankScore = new int[(int)JudgeRank.Max]
    {
        2, 1, 0, 0
    };

    // 判定用フレームレート
    private const float TimingRate = 60f; // fps

    // タイミングは+方向-方向どちらも
    public const float TimingPerfect = 1f / TimingRate;
    public const float TimingGreat = 3f / TimingRate;
    public const float TimingGood = 5f / TimingRate;
}