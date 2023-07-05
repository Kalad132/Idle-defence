using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Player;
using UnityEngine.Events;
using UnityEngine.AI;

namespace Enemies
{
    public class Attacker : MonoBehaviour
    {
        [SerializeField] private NavMeshAgent _agent;
        [SerializeField] private float _range;
        [SerializeField] private float _damage;
        [SerializeField] private float _hitDelay;
        [SerializeField] private float _cooldown;

        private Coroutine _attacking;
        private PlayerBody _currentTarget;

        public float Damage => _damage;

        public bool Attacking => _attacking != null;

        public event UnityAction Attacked;

        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent(out PlayerBody player))
            {
                _currentTarget = player;
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.TryGetComponent(out PlayerBody player))
            {
                if (_currentTarget == player)
                    _currentTarget = null;
            }
        }

        private void Update()
        {
            if (_attacking == null)
                if (_currentTarget != null)
                    Attack(_currentTarget);
        }

        private void Attack(PlayerBody player)
        {
            if (player.enabled == false)
                return;
            if (_attacking != null)
            {
                StopCoroutine(_attacking);
            }
            _attacking = StartCoroutine(PlayAttack(player));
        }

        private IEnumerator PlayAttack(PlayerBody player)
        {
            float timer = _hitDelay;
            Attacked?.Invoke();
            _agent.isStopped = true;
            while (timer > 0)
            {
                transform.rotation = Quaternion.LookRotation(player.transform.position - transform.position);
                timer -= Time.deltaTime;
                yield return null;
            }
            _agent.isStopped = false;
            if (Vector3.Distance(player.transform.position, transform.position) <= _range)
                player.GetHit(this);
            yield return new WaitForSeconds(_cooldown);
            _attacking = null;
        }

        public void MultiplyDamage(float value)
        {
            _damage *= value;
        }
    }
}