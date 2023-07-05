using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Base
{
    public class FactoryDropEffect : MonoBehaviour
    {
        [SerializeField] private ParticleSystem _particleTemplate;
        [SerializeField] private Transform _factoryBody;
        [SerializeField] private float _time;
        [SerializeField] private float _height;

        private Vector3 _start;
        private float _timer;

        private void Awake()
        {
            _start = _factoryBody.position;
        }

        private void OnEnable()
        {
            _timer = 0;
            _factoryBody.position = _start + Vector3.up * _height;
        }

        private void Update()
        {
            _timer += Time.deltaTime;
            _factoryBody.position = _start + Vector3.up * Mathf.Clamp(1 - (_timer / _time), 0, 1);
            if (_timer> _time)
            {
                ParticleSystem particle = Instantiate(_particleTemplate, transform.position, transform.rotation, transform);
                enabled = false;
            }
        }
    }
}