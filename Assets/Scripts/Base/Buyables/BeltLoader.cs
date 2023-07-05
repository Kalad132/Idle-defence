using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Stacking;

namespace Base
{
    public class BeltLoader : MonoBehaviour
    {
        [SerializeField] private Belt _belt;
        [SerializeField] private float _rate;
        [SerializeField] private Bag _bag;

        private float _timer;

        private void Update()
        {
            _timer -= Time.deltaTime;
            if (_timer > 0)
                return;
            Resourse resourse = _bag.TryRemove(new ResourseId(0));
            if (resourse == null)
                return;
            _belt.Transport(resourse);
            _timer = 1 / _rate;
        }
    }
}