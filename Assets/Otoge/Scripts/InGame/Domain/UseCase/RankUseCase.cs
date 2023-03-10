using UnityEngine;

public class RankUseCase : MonoBehaviour
{
    public string GetRank()
    {
        var rank = ();
        
        switch (rank)
        {
            case GameDefine.JudgeRank.Perfect:
                return GameText.RANK_PERFECT;
            case GameDefine.JudgeRank.Great:
                return GameText.RANK_GREAT;
            case GameDefine.JudgeRank.Good:
                return GameText.RANK_GOOD;
            default:
                return GameText.RANK_MISS;
        }
    }
}
