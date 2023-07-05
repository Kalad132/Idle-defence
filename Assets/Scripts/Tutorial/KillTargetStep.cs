using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Shooting;

namespace Tutorial
{
    public class KillTargetStep : TutorialStep
    {
        [SerializeField] private Target _enemy;

        private bool _complete;

        private void Awake()
        {
            enabled = false;
        }

        private void OnEnable()
        {
            _enemy.Died += OnDeath;
            TryComplete();
        }

        private void OnDisable()
        {
            _enemy.Died -= OnDeath;
        }

        private void OnDeath()
        {
            TryComplete();
        }

        private void TryComplete()
        {
            if (_complete == true)
                return;
            if (_enemy.Alive == false || _enemy == null)
            {
                _complete = true;
                Complete();
            }
        }
    }
}