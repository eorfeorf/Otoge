using TMPro;
using UnityEngine;

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
