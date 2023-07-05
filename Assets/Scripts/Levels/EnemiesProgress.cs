using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Enemies;

namespace Levels
{
    public class EnemiesProgress : Progress
    {
        [SerializeField] private AllEnemies _enemies;
        [SerializeField] private Spawner _spawner;

        private void OnEnable()
        {
            _enemies.Added += OnEnemiesCountChanged;
            _enemies.Removed += OnEnemiesCountChanged;
        }

        private void OnDisable()
        {
            _enemies.Added -= OnEnemiesCountChanged;
            _enemies.Removed -= OnEnemiesCountChanged;
        }

        private void OnEnemiesCountChanged(Enemy enemy)
        {
            float target = 1f - (float)_enemies.Count / _spawner.Max;
            Set(target);
        }
    }
}