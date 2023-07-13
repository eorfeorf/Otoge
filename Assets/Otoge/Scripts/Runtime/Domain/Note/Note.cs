
namespace Otoge.Domain
{
    /// <summary>
    /// ノーツデータ(共有データ).
    /// </summary>
    public class Note
    {
        public NoteType Type;
        public bool Active;
        public int Lane;
        public float Time;
        public int PairId;
        public int Size; // ノーツの横幅

        public int UId;
    }   
}