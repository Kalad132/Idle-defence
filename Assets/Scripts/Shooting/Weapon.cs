using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Shooting
{
    public abstract class Weapon : MonoBehaviour
    {
        [SerializeField] private float _fireRate;
        [SerializeField] private int _ammo;
        [SerializeField] private RuntimeAnimatorController _controller;
        [SerializeField] private WeaponView _view;

        public RuntimeAnimatorController Controller => _controller;
        public WeaponView View => _view;
        public float Firerate => _fireRate;
        public bool HasAmmo => _ammo > 0;

        public event UnityAction Shooted; 

        private void OnDestroy()
        {
            Destroy(_view.gameObject);
        }

        public void Shoot(Targeter targeter, Transform agressionSource, float damageMult = 1)
        {
            MakeShot(targeter, agressionSource, damageMult);
            Shooted?.Invoke();
            _ammo--;
        }

        protected abstract void MakeShot(Targeter targeter, Transform aggressionSource, float damageMult);
    }
}