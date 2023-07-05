using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Base;

namespace Tutorial
{
    public class BuyStep : TutorialStep
    {
        [SerializeField] private Buyable _byable;

        private bool _complete;

        private void Awake()
        {
            enabled = false;
        }

        private void OnEnable()
        {
            _byable.Bought += OnBought;
            TryComplete();
        }

        private void OnDisable()
        {
            _byable.Bought -= OnBought;
        }

        private void OnBought()
        {
            TryComplete();
        }

        private void TryComplete()
        {
            if (_complete == true)
                return;
            if (_byable.IsBougth)
            {
                _complete = true;
                Complete();
            }
        }
    }
}