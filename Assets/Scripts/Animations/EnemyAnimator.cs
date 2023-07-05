using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Enemies;
using UnityEngine.AI;

namespace Animations
{
    public class EnemyAnimator : MonoBehaviour
    {
        [SerializeField] private Animator _animator;
        [SerializeField] private Attacker _attacker;
        [SerializeField] private NavMeshAgent _agent;
        [SerializeField] private EnemyThrow _throw;
        [SerializeField] private Summoner _summoner;

        private void OnEnable()
        {
            _attacker.Attacked += OnAttacked;
            if(_throw != null)
                _throw.Thrown += OnThrow;
            if (_summoner != null)
                _summoner.Summoning += OnSummoning;
        }

        private void OnDisable()
        {
            _attacker.Attacked -= OnAttacked;
            if (_throw != null)
                _throw.Thrown -= OnThrow;
            if (_summoner != null)
                _summoner.Summoning -= OnSummoning;

        }

        private void Update()
        {
                _animator.SetBool(Parameters.Standing, _agent.velocity.magnitude < 0.5f);
        }

        private void OnAttacked()
        {
            _animator.SetTrigger(Parameters.Attack);
        }

        private void OnThrow()
        {
            _animator.SetTrigger(Parameters.Throw);
        }

        private void OnSummoning()
        {
            _animator.SetTrigger(Parameters.Summon);
        }

        public class Parameters
        {
            public static string Standing = nameof(Standing);
            public static string Attack = nameof(Attack);
            public static string Throw = nameof(Throw);
            public static string Summon = nameof(Summon);
        }
    }
}