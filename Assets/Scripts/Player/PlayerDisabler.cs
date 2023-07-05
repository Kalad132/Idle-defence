using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Shooting;

namespace Player
{
    public class PlayerDisabler : MonoBehaviour
    {
        [SerializeField] private PlayerMover _mover;
        [SerializeField] private Targeter _targeter;
        [SerializeField] private PlayerBody _body;
 
        private float _timer;

        public void Disable(float time)
        {
            _mover.enabled = false;
            _targeter.enabled = false;
            _body.enabled = false;
            if (time > _timer)
                _timer = time;
            enabled = true;
        }

        private void Enable()
        {
            _mover.enabled = true;
            _targeter.enabled = true;
            _body.enabled = true;
            enabled = false;
        }

        private void Update()
        {
            _timer -= Time.deltaTime;
            if (_timer < 0)
                Enable();
        }
    }
}