
namespace Otoge.Domain
{
    public class NoteExtensionData
    {
        public int Lane;
    }
    
    
    /// <summary>
    /// ノーツデータ(共有データ).
    /// </summary>
    public class NoteBase
    {
        public NoteType Type;
        public bool Active;
        public float Time;
        public int Size;
        public int Uid;
        
        public NoteExtensionData ExData;
    }   
}