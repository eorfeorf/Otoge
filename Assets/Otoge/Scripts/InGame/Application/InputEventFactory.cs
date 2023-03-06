using UniRx;

/// <summary>
/// InputEventを環境別に生成を分けるクラス.
/// </summary>
public class InputEventFactory
{
    public IInputEvent InputEvent { get; private set; }
    
    public InputEventFactory(CompositeDisposable disposable)
    {
        //InputEvent = new InputEventPlayerPC(disposable);
    }
}
