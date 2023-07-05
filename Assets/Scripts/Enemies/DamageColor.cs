using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Shooting;

namespace Enemies
{
    public class DamageColor : MonoBehaviour
    {
        [SerializeField] private Target _target;
        [SerializeField] private Renderer _renderer;
        [SerializeField] private float _targetValue;
        [SerializeField] private float _applyTime;
        [SerializeField] private float _returnTime;
        [SerializeField] private string _materialFloatName;

        private Coroutine _changing;
        private float _start;
        private Color _startColor;

        private void Awake()
        {
            Material material = new Material(_renderer.material);
            _renderer.material = material;
            _startColor = _renderer.material.color;
            _start = _renderer.material.GetFloat(_materialFloatName);
        }

        private void OnEnable()
        {
            _target.Hit += Animate;
        }

        private void OnDisable()
        {
            _target.Hit -= Animate;
            if (_changing != null)
                StopCoroutine(_changing);
        }

        private void Animate()
        {
            ApplyColor();
            Invoke(nameof(ReturnColor), _applyTime);
        }

        private void ApplyColor()
        {
            StartChangeColor(_targetValue, Color.white, _applyTime);
        }

        private void ReturnColor()
        {
            StartChangeColor(_start, _startColor, _returnTime);
        }

        private void StartChangeColor(float target, Color color, float time)
        {
            if (_changing != null)
                StopCoroutine(_changing);
            _changing = StartCoroutine(ChangeColor(target, color, time));
        }

        private IEnumerator ChangeColor(float target, Color color, float time)
        {
            float timer = 0;
            float start = _renderer.material.GetFloat(_materialFloatName);
            Color startColor = _renderer.material.color;
            while (timer < time)
            {
                timer += Time.deltaTime;
                float current = Mathf.Lerp(start, target, timer / time);
                _renderer.material.SetFloat(_materialFloatName, current);
                _renderer.material.color = Color.Lerp(startColor, color, timer / time);
                yield return null;
            }
        }
    }
}