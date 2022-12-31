using System.Collections;
using System.Collections.Generic;
using UniRx;
using UnityEngine;

/// <summary>
/// 入力をコマンドに変換.
/// </summary>
public class InputCommand
{
    public IReadOnlyReactiveProperty<int> Touch => touch;
    private ReactiveProperty<int> touch = new();

    public IReadOnlyReactiveProperty<int> Hold => hold;
    private ReactiveProperty<int> hold = new();

    public InputCommand(IInputEvent inputEvent)
    {
        inputEvent.Touch
    }
}
