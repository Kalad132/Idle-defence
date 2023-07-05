using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Events;

namespace Enemies
{
    public class Summoner : MonoBehaviour
    {
        [SerializeField] private Aggression _aggression;
        [SerializeField] private NavMeshAgent _agent;
        [SerializeField] private float _spawnRate;
        [SerializeField] private Attacker _attacker;
        [SerializeField] private ParticleSystem _spawnAreaTemplate;
        [SerializeField] private ParticleSystem[] _effects;
        [SerializeField] private float _delay;

        private Spawner _spawner;
        private float _cooldown;
        private Coroutine _summoning;

        public event UnityAction Summoning;
        private void Awake()
        {
            ResetCooldown();
            _spawner = FindObjectOfType<Spawner>();
        }

        private void Update()
        {
            _cooldown -= Time.deltaTime;
            if (_cooldown < 0)
            {
                ResetCooldown();
                if (_aggression.IsChasing == false && _spawner.CanSpawn)
                {
                    StartSummon();
                }
            }
        }

        private void StartSummon()
        {
            if (_summoning != null)
            {
                StopCoroutine(_summoning);
            }
            _summoning = StartCoroutine(Summon());
        }

        private IEnumerator Summon()
        {
            Vector3 position = _spawner.RandomRespawn;
            ParticleSystem area = Instantiate(_spawnAreaTemplate, position, Quaternion.identity);
            _agent.isStopped = true;
            _attacker.enabled = false;
            foreach (ParticleSystem particle in _effects)
                particle.Play();
            Summoning?.Invoke();

            yield return new WaitForSeconds(_delay);

            _agent.isStopped = false;
            _attacker.enabled = true;
            foreach (ParticleSystem particle in _effects)
                particle.Stop();
            Destroy(area.gameObject);
            _spawner.TrySpawn(position);
            _summoning = null;
        }

        private void ResetCooldown()
        {
            _cooldown = 1 / _spawnRate;
        }
    }
}