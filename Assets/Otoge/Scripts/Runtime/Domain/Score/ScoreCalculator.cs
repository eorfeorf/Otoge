namespace Otoge.Domain
{
    /// <summary>
    /// スコア計算.
    /// </summary>
    public class ScoreCalculator
    {
        /// <summary>
        /// 最終的なスコア計算.
        /// </summary>
        /// <returns></returns>
        public static int Calc(GameDefine.JudgeRank rank)
        {
            var score = GameDefine.JudgeRankScore[(int) rank];
            return score;
        }
    }
}