using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Base
{
    public class Upgrades : MonoBehaviour
    {
        [SerializeField] private List<Buyable> _buyables;
        [SerializeField] private ParticleSystem _effect;

        private int _upgradeLevel;

        public event UnityAction<int> UpgradeLevelChanged;

        private void Awake()
        {
            foreach (Buyable buyable in _buyables)
                buyable.gameObject.SetActive(false);
            if (_buyables.Count > 0)
                _buyables[0].gameObject.SetActive(true);
        }

        private void OnEnable()
        {
            foreach (Buyable buyable in _buyables)
                buyable.Bought += OnBought;
        }

        private void OnDisable()
        {
            foreach (Buyable buyable in _buyables)
                buyable.Bought -= OnBought;
        }

        private void OnBought()
        {
            _effect.Play();
            _buyables[_upgradeLevel].gameObject.SetActive(false);
            _upgradeLevel++;
            UpgradeLevelChanged?.Invoke(_upgradeLevel);
            if (_buyables.Count > _upgradeLevel)
                _buyables[_upgradeLevel].gameObject.SetActive(true);
        }
    }
}