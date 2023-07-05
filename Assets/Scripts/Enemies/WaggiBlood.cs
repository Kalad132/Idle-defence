using UnityEngine;
using Shooting;

namespace Enemies
{
    public class WaggiBlood : MonoBehaviour
    {
        [SerializeField] private ParticleSystem _bloodTemplate;
        [SerializeField] private ParticleSystem _deathTemplate;
        [SerializeField] private Target _target;

        private void OnEnable()
        {
            _target.Hit += OnHit;
            _target.Died += OnDeath;
        }

        private void OnDisable()
        {
            _target.Hit -= OnHit;
            _target.Died -= OnDeath;
        }

        private void OnHit()
        {
            Instantiate(_bloodTemplate, transform.position, Quaternion.identity);
        }

        private void OnDeath()
        {
            Instantiate(_deathTemplate, transform.position, Quaternion.identity);
        }
    }
}