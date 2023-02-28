using UnityEngine;

/// <summary>
/// ノーツの描画に必要な情報.
/// </summary>
public class NoteView
{
    public GameObject GameObject { get; private set; }
    public Transform Transform { get; private set; }
    public float Time { get; private set; }
    public int LaneIndex { get; private set; }

    public NoteView(Transform transform, float time, int lane)
    {
        Transform = transform;
        GameObject = transform.gameObject;
        Time = time;
        LaneIndex = lane;
    }
}