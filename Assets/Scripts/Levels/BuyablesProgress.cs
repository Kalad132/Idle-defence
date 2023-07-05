using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Levels;

namespace Base
{
    public class BuyablesProgress : Progress
    {
        [SerializeField] private Buyables _buyables;

        private void OnEnable()
        {
            _buyables.BoughtCountChanged += OnCountChanged;
        }

        private void OnDisable()
        {
            _buyables.BoughtCountChanged -= OnCountChanged;
        }

        private void OnCountChanged()
        {
            Set((float)_buyables.BoughtCount / _buyables.Count);
        }
    }
}