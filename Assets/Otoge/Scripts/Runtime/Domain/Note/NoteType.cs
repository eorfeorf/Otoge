
namespace Otoge.Domain
{
    /// <summary>
    /// ノーツタイプ.
    /// </summary>
    public enum NoteType
    {
        // 単発ノーツ.
        Tap,

        // ホールドノーツ.
        HoldStart,
        HoldEnd,
    }
}