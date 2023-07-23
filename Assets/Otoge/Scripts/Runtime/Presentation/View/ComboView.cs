using TMPro;
using UnityEngine;

namespace Otoge.Presentation
{
    public class ComboView : MonoBehaviour
    {
        [SerializeField]
        private TextMeshPro comboText;

        /// <summary>
        /// コンボ数変化.
        /// </summary>
        /// <param name="value"></param>
        public void ChangedCombo(int value)
        {
            comboText.text = value.ToString();
            comboText.gameObject.SetActive(value != 0);
        }
    }
}