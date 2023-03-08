using UnityEngine;

namespace Otoge.Scripts.InGame.Application.Note
{
    public class NoteEntity
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
