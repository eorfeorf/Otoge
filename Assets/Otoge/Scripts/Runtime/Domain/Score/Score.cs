using UniRx;
using UnityEngine;
using VContainer;

namespace Otoge.Domain
{
    public class A : ScriptableObject
    {
    
    }
    
    /// <summary>
    /// インゲーム内のスコア.
    /// </summary>
    public class Score
    {
        /// <summary>
        /// 値が変更された.
        /// </summary>
        public IReadOnlyReactiveProperty<int> OnValueChanged => _onValueChanged;
        private readonly ReactiveProperty<int> _onValueChanged = new();
        
        [Inject]
        public Score(InGamePlayer inGamePlayer, LifeCycle _lifeCycle)
        {
            // ノーツランク適用.
            inGamePlayer.OnApplyNote.SkipLatestValueOnSubscribe().Subscribe(data =>
            {
                Add(ScoreCalculator.Calc(data.Rank));
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
            _onValueChanged.Value += addCount;
        }

        /// <summary>
        /// リセット.
        /// </summary>
        private void Reset()
        {
            _onValueChanged.Value = 0;
        }
    }
}