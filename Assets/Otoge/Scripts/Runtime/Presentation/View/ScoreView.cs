using TMPro;
using UnityEngine;

namespace Otoge.Presentation
{
    public class ScoreView : MonoBehaviour
    {
        [SerializeField]
        private TextMeshProUGUI scoreText;

        /// <summary>
        /// スコア変化.
        /// </summary>
        /// <param name="score"></param>
        public void ChangeScore(int score)
        {
            scoreText.text = score.ToString();
        }
    }
}