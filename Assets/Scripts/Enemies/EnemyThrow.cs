using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Events;

namespace Enemies
{
    public class EnemyThrow : MonoBehaviour
    {
        [SerializeField] private NavMeshAgent _agent;
        [SerializeField] private Attacker _attacker;
        [SerializeField] private float _projectleSpawnDelay;
        [SerializeField] private float _postSpawnDelay;
        [SerializeField] private float _cooldown;
        [SerializeField] private ThrowProjectle _projectleTemplate;
        [SerializeField] private Transform _projectleSpawnPoint;
        [SerializeField] private float _maxRange;

        private Coroutine _throwing;
        private ThrowTarget[] _targets;
        private float _timer;

        public event UnityAction Thrown;

        private void Awake()
        {
            _targets = FindObjectsOfType<ThrowTarget>();
            ResetCooldown();
        }

        private void Update()
        {
            if (_attacker.Attacking)
                return;
            _timer -= Time.deltaTime;
            if (_timer < 0)
            {
                ResetCooldown();
                TryThrow(GetRandon());
            }
        }

        private void TryThrow(ThrowTarget target)
        {
            if (Vector3.Distance(target.transform.position, transform.position) > _maxRange)
                return;
            if (_throwing != null)
            {
                StopCoroutine(_throwing);
            }
            _throwing = StartCoroutine(PlayAnimation(target));
        }

        private IEnumerator PlayAnimation(ThrowTarget target)
        {
            float timer = _projectleSpawnDelay;
            Thrown?.Invoke();
            _agent.isStopped = true;
            _attacker.enabled = false;
            while (timer > 0)
            {
                transform.rotation = Quaternion.LookRotation(target.transform.position - transform.position);
                timer -= Time.deltaTime;
                yield return null;
            }
            ThrowProjectle projectle = Instantiate(_projectleTemplate, _projectleSpawnPoint.position, transform.rotation);
            projectle.Init(target);
            yield return new WaitForSeconds(_postSpawnDelay);
            _agent.isStopped = false;
            _attacker.enabled = true;
            _throwing = null;
        }

        private ThrowTarget GetRandon()
        {
            int index = Random.Range(0, _targets.Length);
            return _targets[index];
        }

        private void ResetCooldown()
        {
            _timer = _cooldown * Random.Range(0.5f, 1.5f);
        }
    }
}