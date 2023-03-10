using TMPro;
using UnityEngine;

public class RankView : MonoBehaviour
{
    [SerializeField]
    TextMeshPro rankText;

    public string Rank
    {
        set => rankText.text = value;
    }
    
}
