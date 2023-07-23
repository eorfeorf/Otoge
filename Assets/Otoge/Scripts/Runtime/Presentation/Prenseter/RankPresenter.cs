using Otoge.Domain;
using Otoge.Domain.Rank;
using UniRx;
using VContainer.Unity;

namespace Otoge.Presentation
{
    public class RankPresenter : IInitializable
    {
        public RankPresenter(Rank rank, RankView view, LifeCycle lifeCycle)
        {
            rank.OnValueChanged.SkipLatestValueOnSubscribe().Subscribe(value =>
            {
                view.ApplyRankText(value);    
            }).AddTo(lifeCycle.CompositeDisposable);
        }

        public void Initialize()
        {
        }
    }
}