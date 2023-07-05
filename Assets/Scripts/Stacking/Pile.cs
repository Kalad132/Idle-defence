using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Stacking
{
    public class Pile : MonoBehaviour
    {
        private List<Resourse> _resourses;

        private void Awake()
        {
            _resourses = new List<Resourse>();
        }

        public bool Contains(Resourse resourse)
        {
            return _resourses.Contains(resourse);
        }

        public void Add(Resourse resourse)
        {
            _resourses.Add(resourse);
            SetTransform(resourse);
        }

        public void Remove(Resourse resourse)
        {
            OnDrop(resourse);
            _resourses.Remove(resourse);
        }

        private void SetTransform(Resourse resourse)
        {
            int index = _resourses.IndexOf(resourse);
            Quaternion offsetRotation = Quaternion.identity;
            Vector3 offsetPosition = CalculateHeight(index) * Vector3.up;
            resourse.transform.parent = transform;
            resourse.AnimateMove(transform, offsetPosition, offsetRotation);
        }

        private void OnDrop(Resourse resourse)
        {
            int index = _resourses.IndexOf(resourse);
            for (int i = index + 1; i < _resourses.Count; i++)
            {
                Quaternion offsetRotation = Quaternion.identity;
                Vector3 offsetPosition = (CalculateHeight(i - 1)) * Vector3.up;
                _resourses[i].AnimateMove(transform, offsetPosition, offsetRotation, true);
            }
        }

        private float CalculateHeight(int index)
        {
            float height = 0;
            for (int i = 0; i < index; i++)
                height += _resourses[i].Size.y;
            return height;
        }
    }
}