using TMPro;
using UnityEngine;

namespace Otoge.Scripts2.Presentation.Views
{
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
}

View と ViewModel に切り離す