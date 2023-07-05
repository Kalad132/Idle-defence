using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace Player
{
    public class PlayerMover : MonoBehaviour
    {
        [SerializeField] private Joystick _joystic;
        [SerializeField] private NavMeshAgent _agent;
        [SerializeField] private float _speed;
        [SerializeField] private Camera _camera;
        [SerializeField] private Transform _view;

        public bool FreeRotation = true;

        private Vector3 _right;
        private Vector3 _up;
        private void OnEnable()
        {
            _agent.isStopped = true;
        }

        private void OnDisable()
        {
            _agent.isStopped = false;
        }


        private void Awake()
        {
            _up = Vector3.ProjectOnPlane(_camera.transform.forward, Vector3.up).normalized;
            _right = Vector3.ProjectOnPlane(_camera.transform.right, Vector3.up).normalized;
        }

        private void Update()
        {
            Vector3 direction = _joystic.Vertical * _up + _joystic.Horizontal * _right;
            Vector3 offset = direction * _speed * Time.deltaTime;
            _agent.Move(offset);
            if (direction.magnitude > 0.1f && FreeRotation)
                _view.rotation = Quaternion.LookRotation(direction);
        }

        public void Teleport(Vector3 position)
        {
            _agent.Warp(position);
        }
    }
}