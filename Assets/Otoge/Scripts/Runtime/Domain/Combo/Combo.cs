using UniRx;

namespace Otoge.Domain
{
    public class Combo
    {
        /// <summary>
        /// 値が変更された.
        /// </summary>
        public IReactiveProperty<int> Value => _value;
        private readonly ReactiveProperty<int> _value = new();

        public Combo(InGamePlayer inGamePlayer, LifeCycle lifeCycle)
        {
            // ノーツ適用.
            inGamePlayer.OnApplyNote.SkipLatestValueOnSubscribe().Subscribe(data =>
            {
                Add();
            }).AddTo(lifeCycle.CompositeDisposable);
        }
        
        /// <summary>
        /// コンボ追加
        /// </summary>
        /// <param name="add"></param>
        private void Add(int add = 1)
        {
            _value.Value += add;
        }

        /// <summary>
        /// リセット.
        /// </summary>
        public void Reset()
        {
            _value.Value = 0;
        }
    }
}
