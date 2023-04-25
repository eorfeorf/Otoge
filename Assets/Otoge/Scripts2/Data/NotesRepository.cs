using System.Collections.Generic;
using Otoge.Scripts2.Domain.Entities;
using Otoge.Scripts2.Domain.Note;

namespace Otoge.Scripts2.Data
{
    /// <summary>
    /// UseCaseで扱うノーツデータ保管クラス.
    /// </summary>
    public class NotesRepository : INotesRepository
    {
        public Dictionary<int, Note> Notes => notes;
        private Dictionary<int, Note> notes = new Dictionary<int, Note>();
        
        public NotesRepository()
        {  
        }
        
        public bool Load(int musicalScoreId)
        {
            // TODO:BPMを楽曲データから持ってくる.
            float startTime = 1f;
            var bpm = GameDefine.BPM;
            var spb = GameDefine.SEC60 / bpm; // secondsPerBeat
            for (int i = 0; i < GameDefine.NOTE_NUM; ++i)
            {
                // 譜面情報からわかる.
                var time = (i * spb) + startTime;
                var note = new Note
                {
                    Type = NoteType.Tap,
                    Active = true,
                    Lane = i % GameDefine.LANE_NUM,
                    Time = time,
                    PairId = i,
                    UId = i,
                };
                notes.Add(note.UId, note);
            }

            return false;
        }
    }
}