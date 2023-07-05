using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Shooting
{
    public class WeaponView : MonoBehaviour
    {
        [SerializeField] private Transform _shootPoint;
        [SerializeField] private Transform _shellPoint;
        [SerializeField] private bool _playerOwned;

        public Transform ShootPoint => _shootPoint;
        public Transform ShellPoint => _shellPoint;

        private void Awake()
        {
            if (_playerOwned == false)
                gameObject.SetActive(false);
        }
    }
}