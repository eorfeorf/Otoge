using UnityEngine;

namespace Otoge.Scripts2.Presentation.Views
{
    public class BarView : MonoBehaviour
    {
        public float Time { get; private set; }

        public void Initialize(float time)
        {
            Time = time;
        }
    }
}