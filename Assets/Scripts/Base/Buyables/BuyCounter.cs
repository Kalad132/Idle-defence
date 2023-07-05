using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Stacking;
using TMPro;

namespace Base
{
    public class BuyCounter : MonoBehaviour
    {
        [SerializeField] private Bag _bag;
        [SerializeField] private Buyable _buyable;
        [SerializeField] private TMP_Text _tmp;

        private void Start()
        {
            UpdateValue();
        }

        private void OnEnable()
        {
            _bag.Added += OnResourseAdded;
        }

        private void OnDisable()
        {
            _bag.Added -= OnResourseAdded;
        }

        private void OnResourseAdded(Resourse resourse)
        {
            UpdateValue();
        }

        private void UpdateValue()
        {
            _tmp.text = (_buyable.Cost - _bag.Count).ToString();
        }
    }
}