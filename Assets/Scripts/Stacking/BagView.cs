using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Stacking
{
    public class BagView : MonoBehaviour
    {
        public const float AnimationTime = 1f;

        [SerializeField] private Bag _bag;
        [SerializeField] private List<Pile> _piles;

        private void OnEnable()
        {
            _bag.Added += OnResourseAdded;
            _bag.Removed += OnResourseRemoved;
        }

        private void OnDisable()
        {
            _bag.Added -= OnResourseAdded;
            _bag.Removed -= OnResourseRemoved;
        }

        private void OnResourseAdded(Resourse resourse)
        {
            int index = _bag.Count - 1;
            int pileIndex = index % _piles.Count;
            _piles[pileIndex].Add(resourse);
        }

        private void OnResourseRemoved(Resourse resourse)
        {
            foreach (Pile pile in _piles)
            {
                if (pile.Contains(resourse))
                    pile.Remove(resourse);
            }
        }
    }
}