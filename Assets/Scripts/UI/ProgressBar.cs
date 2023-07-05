using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using Enemies;
using Levels;

namespace UI
{
    public class ProgressBar : MonoBehaviour
    {
        [SerializeField] private Slider _slider;
        [SerializeField] private Progress _progress;
        [Min(0)]
        [SerializeField] private float _changeTime;

        private Coroutine _changing;

        private void OnEnable()
        {
            _progress.ValueChanged += OnProgressChanged;
        }

        private void OnDisable()
        {
            _progress.ValueChanged -= OnProgressChanged;
        }

        private void OnProgressChanged(float value)
        {
            if (_changing != null)
                StopCoroutine(_changing);
            _changing = StartCoroutine(Change(value));
        }

        private IEnumerator Change(float target)
        {
            float start = _slider.value;
            float timer = 0;
            while (timer < _changeTime)
            {
                timer += Time.deltaTime;
                _slider.value = Mathf.Lerp(start, target, timer / _changeTime);
                yield return null;
            }
        }
    }
}