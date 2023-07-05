using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Stacking;

namespace Base
{
    public abstract class Belt : MonoBehaviour
    {
        [SerializeField] private float _transportTime;
        [SerializeField] private Transform _start;
        [Min(0)]
        [SerializeField] private float _randomness;
        [SerializeField] private Transform _finish;

        private List<Resourse> _transportedResourses = new List<Resourse>();

        public bool IsWorking => _transportedResourses.Count > 0;

        public void Transport(Resourse resourse)
        {
            StartCoroutine(Move(resourse));
        }

        protected abstract void OnMoveEnd(Resourse resourse);

        private IEnumerator Move(Resourse resourse)
        {
            float randomness = Random.Range(-_randomness, _randomness);
            _transportedResourses.Add(resourse);
            Vector3 start = _start.position + _start.transform.right * randomness;
            Vector3 finish = _finish.position + _start.transform.right * randomness;
            resourse.transform.position = start;
            float timer = 0;
            while (timer < _transportTime)
            {
                resourse.transform.position = Vector3.Lerp(start, finish, timer / _transportTime);
                yield return null;
                timer += Time.deltaTime;
            }
            _transportedResourses.Remove(resourse);
            OnMoveEnd(resourse);
        }
    }
}