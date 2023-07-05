using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Shooting;

namespace Enemies
{
    public class Enemy : MonoBehaviour
    {
        [SerializeField] private Target _target;

        public Target Target => _target;

        public event UnityAction<Enemy> Dieing;

        private void OnEnable()
        {
            _target.Died += OnDeath;
        }

        private void OnDisable()
        {
            _target.Died -= OnDeath;
        }

        private void OnDeath()
        {
            Dieing?.Invoke(this);
            Destroy(gameObject);
        }
    }
}