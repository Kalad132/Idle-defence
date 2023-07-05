using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Stacking;

namespace Shooting
{
    public class WeaponChanger : MonoBehaviour
    {
        [SerializeField] private Shooter _shooter;
        [SerializeField] private Bag _bag;

        private void OnEnable()
        {
            _shooter.AskedWeapon += OnWeaponAsked;
        }

        private void OnDisable()
        {
            _shooter.AskedWeapon -= OnWeaponAsked;
        }

        private void OnWeaponAsked()
        {
            Weapon weapon = TryTakeWeapon();
            if (weapon != null)
                _shooter.EquipWeapon(weapon);
        }

        private Weapon TryTakeWeapon()
        {
            Resourse resourse = _bag.TryRemove(new ResourseId(3));
            if (resourse == null)
                return null;
            if (resourse.TryGetComponent(out StockedWeapon Stockedweapon))
            {
                Weapon weapon = Stockedweapon.Weapon;
                weapon.transform.parent = transform;
                Destroy(resourse.gameObject);
                return weapon;
            }
            return null;
        }
    }
}