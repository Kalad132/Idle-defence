using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Enemies
{
    public class ThrowProjectle : MonoBehaviour
    {
        [SerializeField] private AnimationCurve _curve;
        [SerializeField] private float _flyTime;
        [SerializeField] private float _flyHeight;

        private ThrowTarget _target;
        private float _timer;
        private Vector3 _startPosition;

        private void Awake()
        {
            gameObject.SetActive(false); 
        }

        public void Init(ThrowTarget target)
        {
            _target = target;
            _startPosition = transform.position;
            gameObject.SetActive(true);
        }

        private void Update()
        {
            _timer += Time.deltaTime;
            float flyProgress = _timer / _flyTime;
            if (flyProgress <1)
            {
                Vector3 targetPosition = Vector3.Lerp(_startPosition, _target.transform.position, flyProgress);
                targetPosition += Vector3.up * _curve.Evaluate(flyProgress) * _flyHeight;
                transform.position = targetPosition;
            }
            else
            {
                _target.GetHit();
                Destroy(gameObject);
            }
        }
    }
}