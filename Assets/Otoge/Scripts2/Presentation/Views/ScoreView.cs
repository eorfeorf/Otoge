using TMPro;
using UnityEngine;

namespace Otoge.Scripts2.Presentation.Views
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
