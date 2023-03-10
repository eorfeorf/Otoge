using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ComboView : MonoBehaviour
{
    [SerializeField]
    private TextMeshPro comboText;

    /// <summary>
    /// コンボ数変化.
    /// </summary>
    /// <param name="count"></param>
    public void ChangedCombo(int count)
    {
        comboText.text = count.ToString();
        comboText.gameObject.SetActive(count != 0);
    }
}
