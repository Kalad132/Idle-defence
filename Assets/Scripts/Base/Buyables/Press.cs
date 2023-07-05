using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Base
{
    public class Press : MonoBehaviour
    {
        [SerializeField] private float _downDistanse;

        private Vector3 _defaultOffset;
        private Coroutine _moving;

        private Vector3 Defaultposition => _defaultOffset + transform.parent.position;

        private void Awake()
        {
            _defaultOffset = transform.position - transform.parent.position;
        }

        public void MoveToTop(float time)
        {
            if (_moving != null)
                StopCoroutine(_moving);
            _moving = StartCoroutine(MoveTo(Defaultposition.y, time));
        }

        public void MoveToBottom(float time)
        {
            if (_moving != null)
                StopCoroutine(_moving);
            _moving = StartCoroutine(MoveTo(Defaultposition.y - _downDistanse, time));
        }

        private IEnumerator MoveTo(float targetY, float time)
        {
            float timer = 0;
            float startY = transform.position.y;
            while (timer < time)
            {
                timer += Time.deltaTime;
                Vector3 target = transform.position;
                target.y = Mathf.Lerp(startY, targetY, timer / time);
                transform.position = target;
                yield return null;
            }
            Vector3 finalTarget = transform.position;
            finalTarget.y = targetY;
            transform.position = finalTarget;
        }
    }
}