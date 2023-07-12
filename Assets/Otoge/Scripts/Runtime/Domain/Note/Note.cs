using System.Collections.Generic;

/// <summary>
/// ノーツデータ(共有データ).
/// </summary>
public partial class Note
{
    public NoteType Type;
    public bool Active;
    public int Lane;
    public float Time;
    public int PairId;
    public int Size; // ノーツの横幅

    public int UId;
}