using UniRx;
using VContainer;

namespace Otoge.Domain
{
    public class Combo
    {
        /// <summary>
        /// 値が変更された.
        /// </summary>
        public IReactiveProperty<int> OnValueChanged => _onValueChanged;
        private readonly ReactiveProperty<int> _onValueChanged = new();

        [Inject]
        public Combo(InGamePlayer inGamePlayer, LifeCycle lifeCycle)
        {
            // ノーツ適用.
            inGamePlayer.OnApplyNote.SkipLatestValueOnSubscribe().Subscribe(data =>
            {
                Add();
            }).AddTo(lifeCycle.CompositeDisposable);
            
            // ノーツ通過.
            inGamePlayer.OnPassNote.SkipLatestValueOnSubscribe().Subscribe(data =>
            {
                Reset();
            }).AddTo(lifeCycle.CompositeDisposable);
        }
        
        /// <summary>
        /// コンボ追加
        /// </summary>
        /// <param name="add"></param>
        private void Add(int add = 1)
        {
            _onValueChanged.Value += add;
        }

        /// <summary>
        /// リセット.
        /// </summary>
        public void Reset()
        {
            _onValueChanged.Value = 0;
        }
    }
}
