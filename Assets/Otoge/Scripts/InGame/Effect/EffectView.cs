using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

/// <summary>
/// エフェクト表示View.
/// </summary>
public class EffectView : MonoBehaviour
{
    [SerializeField]
    private VisualEffect tapEffectPrefab;

    private readonly List<VisualEffect> tapEffects = new();

    /// <summary>
    /// 初期化.
    /// </summary>
    /// <param name="data"></param>
    /// <param name="lanePositions"></param>
    public void Initialize(int maxLaneNum, List<Vector3> lanePositions)
    {
        // エフェクト
        for (int i = 0; i < maxLaneNum; ++i)
        {
            var vfx = Instantiate(tapEffectPrefab, lanePositions[i], Quaternion.identity, transform);
            tapEffects.Add(vfx);
        }
    }

    /// <summary>
    /// 後処理.
    /// </summary>
    private void OnDestroy()
    {
        tapEffects.Clear();
    }

    /// <summary>
    /// 再生.
    /// </summary>
    /// <param name="laneIndex"></param>
    public void Play(int laneIndex)
    {
        tapEffects[laneIndex].Play();
    }
}
