using UnityEngine;

namespace Otoge.Presentation
{
    public class ViewFactory<T> where T : Object
    {
        private T barPrefab;

        public ViewFactory(T barPrefab)
        {
            this.barPrefab = barPrefab;
        }

        public T Create(Transform parent)
        {
            return Object.Instantiate(barPrefab, parent);
        }
    }
}