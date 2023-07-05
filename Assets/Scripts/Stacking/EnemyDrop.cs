using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Enemies;

namespace Stacking
{
    [RequireComponent(typeof(Enemy))]
    public class EnemyDrop : MonoBehaviour
    {
        [SerializeField] private GameObject _template;
        [SerializeField] private float _maxDropSpeed;

        private Enemy _enemy;

        private void Awake()
        {
            _enemy = GetComponent<Enemy>();
        }

        private void OnEnable()
        {
            _enemy.Dieing += Drop;
            _enemy.Dieing += Drop;
        }

        private void OnDisable()
        {
            _enemy.Dieing -= Drop;
            _enemy.Dieing -= Drop;
        }

        private void Drop(Enemy enemy)
        {
            GameObject resource = Instantiate(_template, transform.position, Quaternion.identity);
            Rigidbody body = resource.GetComponent<Rigidbody>();
            if (body != null)
            {
                Vector3 direction = new Vector3(Random.Range(-1, 1), 3, Random.Range(-1, 1));
                float magnitude = Random.Range(_maxDropSpeed / 2, _maxDropSpeed);
                body.velocity = direction.normalized * magnitude;
            }

        }

    }
}