using UnityEngine;

namespace Otoge.Presentation
{
    /// <summary>
    /// １章節ライン.
    /// </summary>
    public class BarView : MonoBehaviour
    {
        [SerializeField]
        private BarView barPrefab;
        
        // BarViewのファクトリクラス.
        public class Factory
        {
            private BarView barPrefab;

            public Factory(BarView barPrefab)
            {
                this.barPrefab = barPrefab;
            }

            public BarView Create(Transform parent)
            {
                return Instantiate(barPrefab, parent);
            }
        }
    }
}