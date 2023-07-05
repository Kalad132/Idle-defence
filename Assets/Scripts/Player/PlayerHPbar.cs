using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Player
{
    public class PlayerHPbar : MonoBehaviour
    {
        [SerializeField] private Slider _slider;
        [SerializeField] private PlayerHP _hp;
        [SerializeField] private CanvasGroup _group;
        [SerializeField] private Vector3 _offset;
        [SerializeField] private float _changeTime;
        [Range(0, 0.5f)]
        [SerializeField] private float _fadePeriod;

        private Coroutine _changing;
        private Coroutine _changingAlpha;

        private void Awake()
        {
            transform.parent = null;
        }

        private void OnEnable()
        {
            _hp.ValueChanged += OnHpChanged;
        }

        private void OnDisable()
        {
            _hp.ValueChanged -= OnHpChanged;
        }

        private void Update()
        {
            transform.position = _hp.transform.position + _offset;
            transform.rotation = Quaternion.LookRotation(Camera.main.transform.forward);
        }

        private void OnHpChanged(float value)
        {
            if (_changing != null)
                StopCoroutine(_changing);
            _changing = StartCoroutine(Change(value/_hp.Max));
        }

        private IEnumerator Change(float target)
        {
            StartChangeAlpha(1, _fadePeriod);
            float start = _slider.value;
            float timer = 0;
            while (timer < _changeTime - _fadePeriod*2)
            {
                timer += Time.deltaTime;
                float progress = timer / (_changeTime - _fadePeriod*2);
                _slider.value = Mathf.Lerp(start, target, progress);
                yield return null;
            }
            StartChangeAlpha(0, _fadePeriod);
        }

        private void StartChangeAlpha(float target, float time)
        {
            if (_changingAlpha != null)
                StopCoroutine(_changingAlpha);
            _changingAlpha = StartCoroutine(ChangeAlpha(target, time));
        }

        private IEnumerator ChangeAlpha(float target, float time)
        {
            float start = _group.alpha;
            float timer = 0;
            while (timer < time)
            {
                timer += Time.deltaTime;
                float progress = timer / time;
                _group.alpha = Mathf.Lerp(start, target, progress);
                yield return null;
            }
        }
    }
}