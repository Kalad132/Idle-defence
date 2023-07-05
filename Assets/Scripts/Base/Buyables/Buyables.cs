using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Base
{
    public class Buyables : MonoBehaviour
    {
        private Buyable[] _buyables;

        public int BoughtCount { get; private set; }
        public int Count { get; private set; }

        public event UnityAction BoughtCountChanged;

        private void Awake()
        {
            _buyables = FindObjectsOfType<Buyable>();
            Count = _buyables.Length;
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
            BoughtCount++;
            BoughtCountChanged?.Invoke();
        }
    }
}