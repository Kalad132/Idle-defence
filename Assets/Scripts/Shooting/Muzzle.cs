using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Shooting
{
    public class Muzzle : MonoBehaviour
    {
        [SerializeField] private ParticleSystem _template;
        [SerializeField] private ParticleSystem _shellTemplate;
        [SerializeField] private Weapon _weapon;

        private void OnEnable()
        {
            _weapon.Shooted += OnShooted;
        }

        private void OnDisable()
        {
            _weapon.Shooted -= OnShooted;
        }

        private void OnShooted()
        {
            Instantiate(_template, _weapon.View.ShootPoint.position, _weapon.View.ShootPoint.rotation, transform);
            if (_shellTemplate != null)
                Instantiate(_shellTemplate, _weapon.View.ShellPoint.position, _weapon.View.ShellPoint.rotation, transform);
        }

    }
}