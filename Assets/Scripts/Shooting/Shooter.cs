using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Shooting
{
    public class Shooter : MonoBehaviour
    {
        [SerializeField] private Weapon _weapon;
        [SerializeField] private Targeter _targeter;
        [SerializeField] private Transform _viewSlot;
        [SerializeField] private Animator _animator;
        [SerializeField] private Transform _aggressionCreator;

        public float DamageMultiplier = 1;
        public float FirerateMultiplier = 1;

        private float _cooldown;

        public bool ReadyToShoot => _weapon != null && _weapon.HasAmmo;

        public event UnityAction Shooted;
        public event UnityAction AskedWeapon;

        public void EquipWeapon(Weapon weapon)
        {
            if (_weapon != null)
                Destroy(_weapon.gameObject);
            _weapon = weapon;
            weapon.transform.parent = transform;
            weapon.transform.position = transform.position;
            weapon.transform.rotation = transform.rotation;
            weapon.View.transform.parent = _viewSlot;
            weapon.View.transform.position = _viewSlot.position;
            weapon.View.transform.rotation = _viewSlot.rotation;
            weapon.View.gameObject.SetActive(true);
            _animator.runtimeAnimatorController = weapon.Controller;
            _targeter.UpdateTarget();
        }

        private void TryShoot(Target target)
        {
            if (target == null)
                return;
            if (ReadyToShoot == false)
            {
                AskedWeapon?.Invoke();
                return;
            }
            _weapon.Shoot(_targeter, _aggressionCreator, DamageMultiplier);
            Shooted?.Invoke();
        }

        private void Update()
        {
            _cooldown -= Time.deltaTime;
            if (_cooldown <= 0)
            {
                ResetCooldown();
                TryShoot(_targeter.CurrentTarget);
            }
        }

        private void ResetCooldown()
        {
            if (ReadyToShoot == false)
                _cooldown = 1f;
            else
                _cooldown = 1 / _weapon.Firerate / FirerateMultiplier;
        }
    }
}