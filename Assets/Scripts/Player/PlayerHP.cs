using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Player
{
    public class PlayerHP : MonoBehaviour
    {
        [SerializeField] private PlayerBody _body;
        [SerializeField] private ParticleSystem _regenEffect;
        [SerializeField] private BagDrop _drop;
        [SerializeField] private PlayerDisabler _disabler;
        [SerializeField] private PlayerMover _mover;
        [SerializeField] private float _respawnTime;
        [SerializeField] private float _max;

        private Vector3 _respawnPoint;
        private float _value;
        private Coroutine _dieying;
        private float  _regenSpeed;

        public float Max => _max;

        public event UnityAction<bool> IsAlive;
        public event UnityAction<float> ValueChanged;

        private void Awake()
        {
            _respawnPoint = transform.position;
            _value = _max;
        }

        private void OnEnable()
        {
            _body.Hit += OnHit;
        }

        private void OnDisable()
        {
            _body.Hit -= OnHit;
        }

        private void OnTriggerEnter(Collider other)
        {
            if(other.TryGetComponent(out HpRegenZone zone))
            {
                SetRegenSpeed(zone.Speed);
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.TryGetComponent(out HpRegenZone zone))
            {
                SetRegenSpeed(0);
            }
        }

        private void Update()
        {
            if (_regenSpeed > 0 && _value < _max)
            {
                _value += _regenSpeed * Time.deltaTime;
                if (_value > _max)
                    _value = _max;
                ValueChanged?.Invoke(_value);
                if (_regenEffect.isPlaying == false)
                    _regenEffect.Play();
            }
            else
            {
                if (_regenEffect.isPlaying)
                    _regenEffect.Stop();
            }
        }

        public void Multiply(float value)
        {
            _value *= value;
            _max *= value;
        }

        private void OnHit(float damage)
        {
            _value -= damage;
            ValueChanged?.Invoke(_value);
            if (_value <= 0)
            {
                _value = 0;
                Die();
            }
        }

        private void Die()
        {
            if (_dieying != null)
            {
                StopCoroutine(_dieying);
            }
            _drop.DropItems(true);
            _dieying = StartCoroutine(PlayDeath());
        }

        private IEnumerator PlayDeath()
        {
            IsAlive?.Invoke(false);
            _disabler.Disable(_respawnTime);
            yield return new WaitForSeconds(_respawnTime);
            _dieying = null;
            _value = _max;
            ValueChanged?.Invoke(_value);
            IsAlive?.Invoke(true);
            _mover.Teleport(_respawnPoint);
        }

        private void SetRegenSpeed(float value)
        {
            _regenSpeed = value;
            if (value == 0 && _value < _max)
                _regenEffect.Stop();
            else
                _regenEffect.Play();
        }

    }
}