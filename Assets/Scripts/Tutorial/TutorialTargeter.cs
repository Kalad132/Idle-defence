using Player;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Tutorial
{
    public class TutorialTargeter : MonoBehaviour
    {
        [SerializeField] private PlayerBody _player;
        [SerializeField] private TutorialSteps _steps;

        public Transform _target;

        private void OnEnable()
        {
            _steps.Started += OnStepStarted;
            _steps.Completed += OnStepsCompleted;
        }

        private void OnDisable()
        {
            _steps.Started -= OnStepStarted;
            _steps.Completed -= OnStepsCompleted;
        }

        private void OnStepStarted(TutorialStep step)
        {
            _target = step.Target;
        }

        private void OnStepsCompleted()
        {
            _target = null;
            gameObject.SetActive(false);
            enabled = false;
        }

        private void Update()
        {
            if (_target == null)
                return;

            float distance = Vector3.Distance(_player.transform.position, _target.position);
            Vector3 scale = transform.localScale;
            if (distance < 2f)
                scale.z = distance / 2f;
            else
                scale.z = 1;
            transform.localScale = scale;


            Vector3 direction = _target.position - _player.transform.position - Vector3.up;
            direction.y = 0;
            direction = direction.normalized;
            transform.position = _player.transform.position + Vector3.up + direction * 1.5f * transform.localScale.z;
            transform.rotation = Quaternion.LookRotation(direction);

        }
    }
}