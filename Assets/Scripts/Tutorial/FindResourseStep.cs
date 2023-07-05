using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Stacking;

namespace Tutorial
{
    public class FindResourseStep : TutorialStep
    {
        [SerializeField] private Bag _bag;
        [SerializeField] private int _resourseIndex;

        private bool _complete;
        private ResourseId _id;

        private void Awake()
        {
            _id = new ResourseId(_resourseIndex);
            enabled = false;
        }

        private void OnEnable()
        {
            _bag.Added += OnResourseAdded;
            TryComplete();
        }

        private void OnDisable()
        {
            _bag.Added -= OnResourseAdded;
        }

        private void OnResourseAdded(Resourse resourse)
        {
            TryComplete();
        }

        private void TryComplete()
        {
            if (_complete == true)
                return;
            if (_bag.Contains(_resourseIndex) && _bag.Count > 0)
            {
                _complete = true;
                Complete();
            }
        }
    }
}