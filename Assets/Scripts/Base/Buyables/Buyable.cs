using UnityEngine;
using Stacking;
using UnityEngine.Events;

namespace Base
{
    public class Buyable : MonoBehaviour
    {
        [SerializeField] private Bag _bag;
        [SerializeField] private int _cost;
        [SerializeField] private GameObject _target;
        [SerializeField] private GameObject _pile;

        public bool IsBougth { get; private set; }
        public int Cost => _cost;

        public event UnityAction Bought;

        private void Awake()
        {
            if (_target!=null)
                _target.SetActive(false);
            _bag.SetMax(Cost);
        }

        private void OnEnable()
        {
            _bag.Added += OnAmountChanged;
        }

        private void OnDisable()
        {
            _bag.Added -= OnAmountChanged;
        }

        private void OnAmountChanged(Resourse resourse)
        {
            if (_bag.Count >= _cost)
                Invoke(nameof(Buy), 0.35f);
        }

        private void Buy()
        {
            if (_target != null)
                _target.SetActive(true);
            Destroy(_pile.gameObject);
            IsBougth = true;
            Bought?.Invoke();
        }
    }
}