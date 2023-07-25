

namespace Otoge.Domain
{
    /// <summary>
    /// 定数.
    /// </summary>
    public static class GameDefine
    {
        /// <summary>
        /// レーン数.
        /// </summary>
        public static int LANE_NUM => 4;

        /// <summary>
        /// ノーツスピード.
        /// </summary>
        public static float NOTE_BASE_SPEED = 5f;

        // TODO:楽曲データから.
        public static float BPM = 222;

        // TODO:譜面データから.
        public static int NOTE_NUM = 50;

        // 4拍分(1小節)
        public static int BEAT_PER_BAR = 4;

        /// <summary>
        /// 60秒(1分間が何秒か).
        /// </summary>
        public static float SEC60 = 60;

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
            
            // 配列のインデックスで使用しているので最後にNoneを追加.
            None,
        }

        /// <summary>
        /// ノーツ判定によるスコア.
        /// </summary>
        public static readonly int[] JudgeRankScore = {
            2, 1, 0, 0
        };

        // 判定用フレームレート
        private const float TimingRate = 60f; // fps

        // タイミングは+方向-方向どちらも
        public const float TimingPerfect = 1f / TimingRate;
        public const float TimingGreat = 3f / TimingRate;
        public const float TimingGood = 5f / TimingRate;
    }
}