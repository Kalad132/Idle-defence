using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameAnalyticsSDK;
using Levels;

namespace Analytics
{
    public class GAEvents : MonoBehaviour
    {
        [SerializeField] private Game _game;

        float _timer = 0f;
        bool _started = false;
        int _current = -1;

        private void OnEnable()
        {
            _game.LevelStarted += OnLevelStarted;
            _game.LevelCompleted += OnLevelCompleted;
        }

        private void OnDisable()
        {
            _game.LevelStarted -= OnLevelStarted;
            _game.LevelCompleted -= OnLevelCompleted;
        }

        private void Update()
        {
            if (_started)
            _timer += Time.deltaTime;
        }

        private void OnLevelStarted(int number)
        {
            _started = true;
            _current = number;
            _timer = 0;
            GameAnalytics.NewProgressionEvent(GAProgressionStatus.Start, "level" + number.ToString());
        }

        private void OnLevelCompleted(int number)
        {
            Dictionary<string, object> customFields = new Dictionary<string, object>
            {
                {"time_spent",  (int)_timer }
            };
            GameAnalytics.NewProgressionEvent(GAProgressionStatus.Complete, "level" + number.ToString(), customFields);
            if (number == _current)
            {
                _started = false;
                _current = -1;
            }
        }
    }
}