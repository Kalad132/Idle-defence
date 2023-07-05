using UnityEngine;
using System.Collections.Generic;
using UnityEngine.Events;
using TMPro;

namespace Enemies
{
    public class AllEnemies : MonoBehaviour
    {
        [SerializeField] private Spawner _spawner;

        private List<Enemy> _enemies;

        public IReadOnlyList<Enemy> Enemies => _enemies;

        public int Count => _enemies.Count;

        public event UnityAction<Enemy> Added;
        public event UnityAction<Enemy> Removed;

        private void Awake()
        {
            _enemies = new List<Enemy>();
        }

        private void OnEnable()
        {
            _spawner.Spawned += Add;
        }

        private void OnDisable()
        {
            _spawner.Spawned -= Add;
        }

        public void Add(Enemy enemy)
        {
            _enemies.Add(enemy);
            enemy.Dieing += Remove;
            Added?.Invoke(enemy);
        }

        public void Remove(Enemy enemy)
        {
            _enemies.Remove(enemy);
            enemy.Dieing -= Remove;
            Removed?.Invoke(enemy);
        }
    }
}