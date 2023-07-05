using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Shooting
{
    public class PelletMove : MonoBehaviour
    {
        private const float Lifetime = 5f;

        [SerializeField] private float _speed;
        [SerializeField] private float _randomness;

        private void Awake()
        {
            _speed += Random.Range(-_randomness, _randomness);
            Invoke(nameof(Kill), Lifetime);
        }

        private void Update()
        {
            transform.position += transform.forward * Time.deltaTime * _speed;
        }

        private void Kill()
        {
            Destroy(gameObject);
        }
    }
}