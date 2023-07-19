namespace Otoge.Domain
{
    public class BarLine
    {
        public BarLine(float bpm, float bgmTime)
        {
            Bpm = bpm;
            BgmTime = bgmTime;
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