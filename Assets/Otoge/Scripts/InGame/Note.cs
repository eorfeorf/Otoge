using System.Collections.Generic;

public enum NoteType
{
    // 単発ノーツ.
    Tap, 
    
    // ホールドノーツ.
    HoldFirst,
    HoldMiddles,
    HoldLast,
}

public class Note
{
    public int Id;
    public int Lane;
    public float Time;
    public bool Active;   
    public NoteType Type;
}