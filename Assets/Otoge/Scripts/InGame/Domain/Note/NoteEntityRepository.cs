using System.Collections.Generic;
using Otoge.Scripts.InGame.Application.Interface;

namespace Otoge.Scripts.InGame.Application.Note
{
    /// <summary>
    /// ノーツエンティティ管理.
    /// </summary>
    public class NoteEntityRepository : INoteEntityRepository
    {
        public IList<NoteEntity> Entities => entities;
        private List<NoteEntity> entities = new List<NoteEntity>();

        public NoteEntityRepository(INoteRepository noteRepository)
        {
            foreach (var data in noteRepository.Notes)
            {
                var entity = new NoteEntity
                {
                    Type = data.Type,
                    Active = data.Active,
                    Lane = data.Lane,
                    Time = data.Time,
                    PairId = data.PairId,
                    Size = data.Size,
                    UId = data.UId
                };
                entities.Add(entity);
            }
        }
    }
}