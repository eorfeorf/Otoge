public static class GameDefine
{
    public enum JudgeRank
    {
        Perfect, Great, Good, Miss
    }
    
    private const float TimingRate = 60f; // fps
    
    // タイミングは+方向-方向どちらも
    public const float TimingPerfect = 1f / TimingRate;
    public const float TimingGreat = 3f / TimingRate;
    public const float TimingGood = 5f / TimingRate;
}