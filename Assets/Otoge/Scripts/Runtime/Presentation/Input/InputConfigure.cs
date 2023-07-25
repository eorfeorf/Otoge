using UnityEngine.InputSystem;

namespace Otoge.Presentation
{
    public struct InputKeyToLaneData
    {
        public Key Key;
        public int LaneIndex;
        
        public InputKeyToLaneData(Key key, int laneIndex)
        {
            Key = key;
            LaneIndex = laneIndex;
        }
    }

    public static class InputConfigure
    {
        /// <summary>
        /// 入力とレーンの割り当て.
        /// TODO:LaneIndexを動的に変更.
        /// </summary>
        public static readonly InputKeyToLaneData[] InputKeyToLane =
        {
            new(Key.S, 0),
            new(Key.F, 1),
            new(Key.J, 2),
            new(Key.L, 3),
        };
    }
}