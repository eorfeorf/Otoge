using UnityEngine;

public class InputEventFactory
{
    public IInputEvent InputEvent { get; private set; }
    
    public InputEventFactory()
    {
        InputEvent = new InputEventPlayerPC();
    }
}
