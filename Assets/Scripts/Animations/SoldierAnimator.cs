using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Shooting;
using UnityEngine.AI;
using Player;

namespace Animations
{
    public class SoldierAnimator : MonoBehaviour
    {
        [SerializeField] private Animator _animator;
        [SerializeField] private Shooter _shooter;
        [SerializeField] private Targeter _targeter;


        private void OnEnable()
        {
            _shooter.Shooted += OnShooted;
            _targeter.TargetChanged += OnTargetChanged;
        }

        private void OnDisable()
        {
            _shooter.Shooted -= OnShooted;
            _targeter.TargetChanged -= OnTargetChanged;
        }

        private void OnTargetChanged(Target target)
        {
            _animator.SetBool(Parameters.Aiming, target != null && _shooter.ReadyToShoot);
        }

        private void OnShooted()
        {
            _animator.SetTrigger(Parameters.Shooted);
            _animator.SetBool(Parameters.Aiming, _shooter.ReadyToShoot);
        }


        public class Parameters
        {
            public static string Shooted = nameof(Shooted);
            public static string Aiming = nameof(Aiming);
            public static string AskedWeapon = nameof(AskedWeapon);
        }
    }
}