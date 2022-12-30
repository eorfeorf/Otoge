using System.Collections;
using System.Collections.Generic;
using UniRx;
using UnityEngine;

public class Combo
{
    public IReactiveProperty<int> Count => count;
    private ReactiveProperty<int> count = new();
    
    public Combo()
    {
        
    }

    /// <summary>
    /// コンボ追加
    /// </summary>
    /// <param name="add"></param>
    public void Add(int add = 1)
    {
        count.Value += add;
    }

    /// <summary>
    /// リセット.
    /// </summary>
    public void Reset()
    {
        count.Value = 0;
    }
}
