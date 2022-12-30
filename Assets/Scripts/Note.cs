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

public struct NotePlayData
{
    public int Id;
    public int Lane;
    public float Time;
    public bool Active;   
    public NoteType Type;
}

public class Note
{
    // Middles, Lastはロングノーツ用
    // 始点と終点しかない場合はMiddlesは使わない.
    public NotePlayData First;
    public List<NotePlayData> Middles;
    public NotePlayData Last;
}