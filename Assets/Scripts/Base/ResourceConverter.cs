using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Stacking;
using UnityEngine.Events;

namespace Base
{
    public class ResourceConverter : MonoBehaviour
    {
        [SerializeField] private Bag _out;
        [SerializeField] private Resourse _targetResourseTemplate;

        public event UnityAction Converted;

        public void Convert(Resourse resource)
        {
            if (resource == null)
                return;
            Resourse newResourse = Instantiate(_targetResourseTemplate, transform.position, Quaternion.identity);
            _out.TryAdd(newResourse);
            resource.AnimateMove(transform, Vector3.zero, Quaternion.identity);
            Destroy(resource.gameObject, Resourse.AnimationTime);
            Converted?.Invoke();
        }
    }
}