using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Base
{
    public class ResourceConverterAnimation : MonoBehaviour
    {
        [SerializeField] private Belt _belt;
        [SerializeField] private float _turnSpeed;
        [SerializeField] private Transform _upperMillstone;
        [SerializeField] private Transform _lowerMillstone;

        [SerializeField] private Renderer _renderer;
        [SerializeField] private float _beltSpeed;

        [SerializeField] private ResourceConverter _converter;
        [SerializeField] private ParticleSystem _particle;


        private void Awake()
        {
            Material material = new Material(_renderer.material);
            _renderer.material = material;
        }

        private void OnEnable()
        {
            _converter.Converted += OnResourseConverted;
        }

        private void OnDisable()
        {
            _converter.Converted -= OnResourseConverted;
        }

        private void Update()
        {
            if (_belt.IsWorking)
            {
                RotateMilestones();
                MoveBelt();
            }
        }

        private void RotateMilestones()
        {
           _upperMillstone.rotation *= Quaternion.Euler(_turnSpeed * Time.deltaTime, 0, 0);
           _lowerMillstone.rotation *= Quaternion.Euler(-_turnSpeed * Time.deltaTime, 0, 0);
        }

        private void MoveBelt()
        {
            Vector2 offset = _renderer.material.mainTextureOffset;
            offset.y += _beltSpeed * Time.deltaTime;
            _renderer.material.mainTextureOffset = offset;
        }

        private void OnResourseConverted()
        {
            _particle.Play();
        }
    }
}