using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
using UnityEngine;
using VContainer;

namespace Otoge.Domain
{
    //public interface 
    
    /// <summary>
    /// 読み込んだノーツの管理.
    /// </summary>
    public class NoteContainer
    {
        // ノーツ全部.
        public IDictionary<int, NoteBase> Notes => _notes;
        private readonly Dictionary<int, NoteBase> _notes = new();

        public readonly List<List<NoteBase>> NotesByLane = new();

        /// <summary>
        /// ノーツデータの生成.
        /// TODO:譜面からデータに変換.
        /// </summary>
        [Inject]
        public NoteContainer()
        {
            for (var i = 0; i < GameDefine.LANE_NUM; ++i)
            {
                NotesByLane.Add(new());    
            }
            
            float startTime = 0f;
            var bpm = GameDefine.BPM;
            var spb = GameDefine.SEC60 / bpm; // secondsPerBeat
            for (int i = 0; i < GameDefine.NOTE_NUM; ++i)
            {
                // 譜面情報からわかる.
                var type = NoteType.Tap;
                var time = i + startTime;
                var lane = i % GameDefine.LANE_NUM;
                var note = Create(type, lane, time, 1, i);
                _notes.Add(note.Uid, note);

                NotesByLane[lane].Add(note);
            }

            Debug.Log("[NoteContainer] Initialized.");
        }

        /// <summary>
        /// ノーツ生成.
        /// </summary>
        /// <param name="type"></param>
        /// <param name="lane"></param>
        /// <param name="time"></param>
        /// <param name="size"></param>
        /// <param name="uid"></param>
        /// <returns></returns>
        private NoteBase Create(NoteType type, int lane, float time, int size, int uid)
        {
            var ex = new NoteExtensionData()
            {
                Lane = lane
            };
            var note = new NoteBase
            {
                Type = type,
                Active = true,
                Time = time,
                Size = size,
                Uid = uid,
                ExData = ex,
            };

            return note;
        }

        /// <summary>
        /// アクティブを設定.
        /// </summary>
        /// <param name="noteBase"></param>
        /// <param name="isActive"></param>
        public void SetActive(NoteBase noteBase, bool isActive)
        {
            _notes[noteBase.Uid].Active = isActive;
            //note.Active = isActive;
        }

        /// <summary>
        /// アクティブを設定.
        /// </summary>
        /// <param name="note"></param>
        /// <param name="isActive"></param>
        public void SetActiveAll(bool isActive)
        {
            foreach (var note in _notes)
            {
                note.Value.Active = isActive;
            }
        }
    }
}