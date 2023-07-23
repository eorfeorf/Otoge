namespace Otoge.Domain
{
    /// <summary>
    /// ノーツ適用時のデータ.
    /// </summary>
    public class NoteApplyData
    {
        public Note Note;
        
        // 得点表示のために事前に判定する.
        public GameDefine.JudgeRank Rank;
    }
}