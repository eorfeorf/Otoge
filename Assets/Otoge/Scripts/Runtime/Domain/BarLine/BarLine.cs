using UnityEngine;

namespace Otoge.Domain
{
    public class BarLine
    {
        public BarLine(float bpm, float bgmTime)
        {
            Bpm = bpm;
            BgmTime = bgmTime;
            
            Debug.Log("[BarLine] Initialized.");
        }
        
        /// <summary>
        /// BPM.
        /// </summary>
        public float Bpm { get; private set; }

        /// <summary>
        /// BGMの時間
        /// </summary>
        public float BgmTime;
    }
}