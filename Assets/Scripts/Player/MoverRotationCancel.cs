using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Shooting;

namespace Player
{
    public class MoverRotationCancel : MonoBehaviour
    {
        [SerializeField] private PlayerMover _mover;
        [SerializeField] private Targeter _targeter;

        private void OnEnable()
        {
            _targeter.TargetChanged += OnTargetChanged;
        }

        private void OnDisable()
        {
            _targeter.TargetChanged -= OnTargetChanged;
        }

        private void OnTargetChanged(Target target)
        {
            _mover.FreeRotation = target == null;
        }
    }
}