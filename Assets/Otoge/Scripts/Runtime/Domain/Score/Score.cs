using UniRx;

namespace Otoge.Domain
{
    /// <summary>
    /// インゲーム内のスコア.
    /// </summary>
    public class Score
    {
        /// <summary>
        /// 値が変更された.
        /// </summary>
        public IReadOnlyReactiveProperty<int> Value => _value;
        private readonly ReactiveProperty<int> _value = new();
        
        public Score(InGamePlayer inGamePlayer, ScoreCalculator scoreCalculator, LifeCycle _lifeCycle)
        {
            // ノーツランク適用.
            inGamePlayer.OnApplyNote.SkipLatestValueOnSubscribe().Subscribe(data =>
            {
                Add(scoreCalculator.Calc(data.Rank));
            }).AddTo(_lifeCycle.CompositeDisposable);
            
            // ノーツが通り過ぎた.
            inGamePlayer.OnPassNote.SkipLatestValueOnSubscribe().Subscribe(note =>
            {
                Reset();
            }).AddTo(_lifeCycle.CompositeDisposable);
        }

        /// <summary>
        /// スコア加算.
        /// </summary>
        /// <param name="addCount"></param>
        private void Add(int addCount)
        {
            _value.Value += addCount;
        }

        /// <summary>
        /// リセット.
        /// </summary>
        private void Reset()
        {
            _value.Value = 0;
        }
    }
}