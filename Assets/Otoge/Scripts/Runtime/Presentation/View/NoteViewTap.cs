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
        public int Size { get; private set; }

        public void Initialize(float time, int lane, int size, InGameViewInfo viewInfo)
        {
            Time = time;
            LaneIndex = lane;
            Size = size;
            var pos = transform.position;
            var x = viewInfo.LanePositionsX[lane];
            x += (size / 2f) * viewInfo.LaneWidth;
            transform.position = new Vector3(x, pos.y, pos.z);
        }
    }
}