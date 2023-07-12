using UniRx;

/// <summary>
/// インゲーム内のスコア.
/// </summary>
public class Score
{
    public IReadOnlyReactiveProperty<int> Count => count;
    private ReactiveProperty<int> count = new();
    
    /// <summary>
    /// スコア加算.
    /// </summary>
    /// <param name="addCount"></param>
    public void Add(int addCount)
    {
        count.Value +=addCount;
    }

    /// <summary>
    /// リセット.
    /// </summary>
    public void Reset()
    {
        count.Value = 0;
    }
}
