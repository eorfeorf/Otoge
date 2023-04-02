using UnityEngine;

namespace Otoge.Scripts2.Presentation.Views
{
    public class NoteView : MonoBehaviour
    {
        public float Time { get; private set; }
        public int LaneIndex { get; private set; }

        public void Initialize(float time, int laneIndex)
        {
            Time = time;
            LaneIndex = laneIndex;
        }
    }
}