using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Levels;

namespace Analytics
{
    public class AppMetricaEvents : MonoBehaviour
    {
        [SerializeField] private Game _game;
        [SerializeField] private Tutorial.TutorialSteps _steps; 

        float _timer = 0f;
        bool _started = false;
        int _current = -1;

        private void OnEnable()
        {
            _game.LevelStarted += OnLevelStarted;
            _game.LevelCompleted += OnLevelCompleted;
            if (_steps != null)
                _steps.StepCompleted += OnTutorialStepCompleted;
        }

        private void OnDisable()
        {
            _game.LevelStarted -= OnLevelStarted;
            _game.LevelCompleted -= OnLevelCompleted;
            if (_steps != null)
                _steps.StepCompleted -= OnTutorialStepCompleted;
        }

        private void Update()
        {
            if (_started)
                _timer += Time.deltaTime;
        }

        private void OnLevelStarted(int number)
        {
            Dictionary<string, object> customFields = new Dictionary<string, object>
            {
                {"level", number},
            };
            AppMetrica.Instance.ReportEvent("level_started", customFields);
            AppMetrica.Instance.SendEventsBuffer();
            _started = true;
            _current = number;
            _timer = 0;
        }

        private void OnLevelCompleted(int number)
        {
            Dictionary<string, object> customFields = new Dictionary<string, object>
            {
                {"level", number},
                {"time_spent",  (int)_timer }
            };
            AppMetrica.Instance.ReportEvent("level_complete", customFields);
            AppMetrica.Instance.SendEventsBuffer();
            if (number == _current)
            {
                _started = false;
                _current = -1;
            }
        }

        private void OnTutorialStepCompleted(int index, string name)
        {
            Dictionary<string, object> customFields = new Dictionary<string, object>
            {
                { "index", index},
                {"name", name},
            };
            AppMetrica.Instance.ReportEvent("tutorial_completed", customFields);
            AppMetrica.Instance.SendEventsBuffer();
        }
    }
}