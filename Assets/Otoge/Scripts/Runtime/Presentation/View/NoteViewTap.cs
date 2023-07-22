using UnityEngine;

namespace Otoge.Presentation
{
    /// <summary>
    /// ノーツの描画に必要な情報.
    /// </summary>
    public class NoteViewTap : MonoBehaviour
    {
        public GameObject GameObject => gameObject;
        public Transform Transform => gameObject.transform;
        
        public float Time { get; private set; }
        public int LaneIndex { get; private set; }

        public void Initialize(float time, int lane)
        {
            Time = time;
            LaneIndex = lane;
        }
    }
}