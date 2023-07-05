using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Levels;
using Shooting;

namespace Enemies
{
    public class Factory : MonoBehaviour
    {
        [SerializeField] private List<Area> _areas;
        [SerializeField] private Enemy _template;
        [SerializeField] private Save _save;

        public IReadOnlyList<Area> Areas => _areas;
        private float _multiplier;

        private void Awake()
        {
            _multiplier = (float)_save.GetLevel() * 0.10f + 1;
        }

        public Enemy Create(Vector3 position)
        {
            Enemy spawned = Instantiate(_template, position, Quaternion.identity, transform);
            spawned.GetComponent<EnemyMover>().Init(_areas);
            spawned.GetComponent<Attacker>().MultiplyDamage(_multiplier);
            spawned.GetComponent<Target>().MultiplyHP(_multiplier);
            return spawned;
        }
    }
}