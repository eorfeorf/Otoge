using System.Collections.Generic;

namespace Otoge.Scripts2.Domain.Note
{
    /// <summary>
    /// ノーツ管理.
    /// </summary>
    public interface INotesRepository
    {
        /// <summary>
        /// ノーツ読み込んだノーツデータ.
        /// </summary>
        public Dictionary<int, Entities.Note> Notes { get; }

        /// <summary>
        /// 読み込み.
        /// </summary>
        /// <returns></returns>
        public bool Load(int musicalScoreId);
    }
}