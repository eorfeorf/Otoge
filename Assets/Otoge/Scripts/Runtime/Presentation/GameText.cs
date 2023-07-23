using System.Collections.Generic;
using Otoge.Domain;

namespace Otoge.Presentation
{
    public static class GameText
    {
        public static readonly Dictionary<GameDefine.JudgeRank, string> RankText = new()
        {
            {GameDefine.JudgeRank.None, ""},
            {GameDefine.JudgeRank.Perfect, "PERFECT"},
            {GameDefine.JudgeRank.Great, "GREAT"},
            {GameDefine.JudgeRank.Good, "GOOD"},
            {GameDefine.JudgeRank.Miss, "MISS"},
        };
    }
}