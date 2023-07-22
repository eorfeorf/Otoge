namespace Otoge.Domain
{
    /// <summary>
    /// インゲームの設定.
    /// </summary>
    public class InGameConfiguration
    {
        public int LaneNum { get; private set; }

        public InGameConfiguration()
        {
            LaneNum = GameDefine.LANE_NUM;
        }
    }
}