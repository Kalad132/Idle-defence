using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Enemies
{
    public class Spawner : MonoBehaviour
    {
        [SerializeField] private int _maxEnemies;
        [SerializeField] private Factory _factory;
        [SerializeField] private Area _respawn;
        [Min(0)]
        [SerializeField] private AllEnemies _all;
        [SerializeField] private List<Enemy> _starting;


        public int Max => _maxEnemies;

        public bool CanSpawn => _all.Enemies.Count < _maxEnemies;

        public Vector3 RandomRespawn => _respawn.RandomPoint;

        public event UnityAction<Enemy> Spawned;

        private void Start()
        {
            foreach (Enemy enemy in _starting)
            {
                _all.Add(enemy);
            }
            while (_all.Count < _maxEnemies)
            {
                int randomIndex = Random.Range(0, _factory.Areas.Count);
                Area area = _factory.Areas[randomIndex];
                Spawn(area.RandomPoint);
            }
        }

        public void TrySpawn(Vector3 position)
        {
            if (CanSpawn == false)
                return;
            Spawn(position);
        }

        private Enemy Spawn(Vector3 position)
        {
            Enemy enemy =  _factory.Create(position);
            Spawned?.Invoke(enemy);
            return enemy;
        }
    }
}