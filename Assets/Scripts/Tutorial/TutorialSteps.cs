using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Tutorial
{
    public class TutorialSteps : MonoBehaviour
    {
        [SerializeField] private List<TutorialStep> _steps;

        private int _currentIndex;

        private TutorialStep Current => _steps[_currentIndex];

        public event UnityAction<TutorialStep> Started;
        public event UnityAction Completed;
        public event UnityAction<int, string> StepCompleted;

        private void OnEnable()
        {
            foreach (TutorialStep step in _steps)
            {
                step.Completed += OnStepCompleted;
            }
        }

        private void OnDisable()
        {
            foreach (TutorialStep step in _steps)
                step.Completed -= OnStepCompleted;
        }

        private void Start()
        {
            StartTutorial();
        }

        private void StartTutorial()
        {
            if (_steps.Count == 0)
            {
                CompleteTutorial();
                return;
            }
            _currentIndex = 0;
            StartStep(Current);
        }

        private void MoveToNextStep()
        {
            Current.enabled = false;
            _currentIndex++;
            if (_currentIndex > _steps.Count - 1)
                CompleteTutorial();
            else
                StartStep(Current);
        }

        private void StartStep(TutorialStep step)
        {
            Started?.Invoke(step);
            step.enabled = true;
        }

        private void OnStepCompleted(TutorialStep step)
        {
            if (step != Current)
                return;
            StepCompleted?.Invoke(_currentIndex, step.name);
            MoveToNextStep();
        }

        private void CompleteTutorial()
        {
            enabled = false;
            Completed?.Invoke();
        }
    }
}