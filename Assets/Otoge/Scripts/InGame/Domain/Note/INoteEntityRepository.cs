using System.Collections.Generic;

namespace Otoge.Scripts.InGame.Application.Note
{
    /// <summary>
    /// ノーツデータから実際に扱えるデータに変換.
    /// </summary>
    public interface INoteEntityRepository
    {
        public IList<NoteEntity> Entities { get; }
    }
}