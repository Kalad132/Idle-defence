using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Player
{
    public class StunEffect : MonoBehaviour
    {
        [SerializeField] private ParticleSystem _particleTemplate;
        [SerializeField] private PlayerBody _player;

        private void OnEnable()
        {
            _player.Stunned += OnStun;
        }

        private void OnDisable()
        {
            _player.Stunned -= OnStun;
        }

        private void OnStun(bool status)
        {
            if (status == false)
                return;
            Instantiate(_particleTemplate, transform.position, transform.rotation);
        }
    }
}