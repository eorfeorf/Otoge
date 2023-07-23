using UniRx;
using VContainer.Unity;

namespace Otoge.Domain.Rank
{
    public class Rank : IInitializable
    {
        public IReadOnlyReactiveProperty<GameDefine.JudgeRank> OnValueChanged => _onValueChanged;
        private readonly ReactiveProperty<GameDefine.JudgeRank> _onValueChanged = new();
        
        public void Initialize()
        {
        }

        public Rank(InGamePlayer inGamePlayer, LifeCycle lifeCycle)
        {
            inGamePlayer.OnApplyNote.SkipLatestValueOnSubscribe().Subscribe(data =>
            {
                _onValueChanged.SetValueAndForceNotify(data.Rank);
            }).AddTo(lifeCycle.CompositeDisposable);
            
            _onValueChanged.SetValueAndForceNotify(GameDefine.JudgeRank.None);
        }
    }
}