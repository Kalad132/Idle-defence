using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Enemies;

namespace Shooting
{
    public class AllTargets : MonoBehaviour
    {
        [SerializeField] private AllEnemies _allEnemies;

        private List<Target> _enemies;

        public IReadOnlyList<Target> All => _enemies;

        private void Awake()
        {
            _enemies = new List<Target>();
        }


        private void OnEnable()
        {
            _allEnemies.Added += Add;
            _allEnemies.Removed += Remove;
        }

        private void OnDisable()
        {
            _allEnemies.Added -= Add;
            _allEnemies.Removed -= Remove;

        }

        private void Add(Enemy enemy)
        {
            _enemies.Add(enemy.Target);
        }

        private void Remove(Enemy enemy)
        {
            if (enemy.TryGetComponent(out Target target))
                _enemies.Remove(target);
        }
    }
}