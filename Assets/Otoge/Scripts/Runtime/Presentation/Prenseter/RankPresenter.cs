using Otoge.Domain;

namespace Otoge.Presentation
{
    public class RankPresenter
    {
        public RankPresenter(RankView view)
        {
            //TODO:仮実装.
            var rank = GameDefine.JudgeRank.Perfect;
            view.ApplyRankText(rank);
        }
    }
}