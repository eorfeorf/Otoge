using Otoge.Scripts2.Domain.Entities;
using TMPro;
using UnityEngine;

public class RankView : MonoBehaviour
{
    [SerializeField]
    private TextMeshPro rankText;
    
    /// <summary>
    /// ランクテキスト反映.
    /// </summary>
    /// <param name="rank"></param>
    public void ApplyRankText(GameDefine.JudgeRank rank)
    {
        switch (rank)
        {
            case GameDefine.JudgeRank.Perfect:
                rankText.text = GameText.RANK_PERFECT;
                break;
            case GameDefine.JudgeRank.Great:
                rankText.text = GameText.RANK_GREAT;
                break;
            case GameDefine.JudgeRank.Good:
                rankText.text = GameText.RANK_GOOD;
                break;
            case GameDefine.JudgeRank.Miss:
                rankText.text = GameText.RANK_MISS;
                break;
        }
    }
}
