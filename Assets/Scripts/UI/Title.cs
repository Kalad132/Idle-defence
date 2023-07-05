using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UI
{
    public class Title : MonoBehaviour
    {
        [SerializeField] private CanvasGroup _canvas;
        [SerializeField] private float _showingTime;

        public void Show()
        {
            StartCoroutine(ChangeAlpha(1));
            _canvas.interactable = true;
            _canvas.blocksRaycasts = true;
        }

        public void Hide()
        {
            StartCoroutine(ChangeAlpha(0));
            _canvas.interactable = false;
            _canvas.blocksRaycasts = false;
        }

        private IEnumerator ChangeAlpha(float target)
        {
            float timer = 0;
            float start = _canvas.alpha;
            while (timer < _showingTime)
            {
                timer += Time.deltaTime;
                _canvas.alpha = Mathf.Lerp(start, target, timer / _showingTime);
                yield return null;
            }
        }
    }
}