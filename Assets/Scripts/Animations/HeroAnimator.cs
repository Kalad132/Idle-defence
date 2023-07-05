using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Shooting;
using UnityEngine.AI;
using Player;

namespace Animations
{
    public class HeroAnimator : MonoBehaviour
    {
        [SerializeField] private Animator _animator;
        [SerializeField] private Shooter _shooter;
        [SerializeField] private Targeter _targeter;
        [SerializeField] private PlayerBody _body;
        [SerializeField] private PlayerHP _hp;

        private Vector3 _lastPosition;

        private void OnEnable()
        {
            _shooter.Shooted += OnShooted;
            _targeter.TargetChanged += OnTargetChanged;
            _body.Stunned += OnPlayerStunned;
            _hp.IsAlive += OnIsAliveChanged;
        }

        private void OnDisable()
        {
            _shooter.Shooted -= OnShooted;
            _targeter.TargetChanged -= OnTargetChanged;
            _body.Stunned -= OnPlayerStunned;
            _hp.IsAlive -= OnIsAliveChanged;
        }

        private void Update()
        {
            SetRunning();
            _lastPosition = transform.position;
        }


        private void OnTargetChanged(Target target)
        {
            _animator.SetBool(Parameters.Aiming, target != null);
        }

        private void OnShooted()
        {
            _animator.SetTrigger(Parameters.Shooted);
        }

        private void OnPlayerStunned(bool stunned)
        {
            if (stunned) 
                _animator.SetTrigger(Parameters.Stunned);
            _animator.SetBool(Parameters.IsStunned, stunned);
        }

        private void SetRunning()
        {
            _animator.SetBool(Parameters.Running, transform.position != _lastPosition);
            Vector3 direction = (_lastPosition - transform.position).normalized;
            if (direction.magnitude < 0.1f * Time.deltaTime)
            {
                direction = Vector3.zero;
            }
            float x = Vector3.Project(direction, transform.forward).magnitude;
            float z = Vector3.Project(direction, transform.right).magnitude;
            _animator.SetFloat(Parameters.DirectionX, x);
            _animator.SetFloat(Parameters.DirectionZ, z);
        }

        private void OnIsAliveChanged(bool isAlive)
        {
            if (isAlive)
                _animator.SetTrigger(Parameters.Reset);
            else
                _animator.SetTrigger(Parameters.Die);
        }

        public class Parameters
        {
            public static string Shooted = nameof(Shooted);
            public static string Aiming = nameof(Aiming);
            public static string Running = nameof(Running);
            public static string Stunned = nameof(Stunned);
            public static string IsStunned = nameof(IsStunned);
            public static string Die = nameof(Die);
            public static string Reset = nameof(Reset);
            public static string DirectionX = nameof(DirectionX);
            public static string DirectionZ = nameof(DirectionZ);
        }
    }
}