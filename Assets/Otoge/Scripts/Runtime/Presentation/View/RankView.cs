using Otoge.Domain;
using TMPro;
using UnityEngine;

namespace Otoge.Presentation
{
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
            rankText.text = GameText.RankText[rank];
        }
    }
}