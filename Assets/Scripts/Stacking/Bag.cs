using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Stacking
{
    public class Bag : MonoBehaviour
    {
        [Tooltip ("0 = any")]
        [SerializeField] private int _fixedResourse = 0;
        [Tooltip("0 = infinite")] [Min (0)]
        [SerializeField] private int _maxAmount = 0;

        private List<Resourse> _resourses = new List<Resourse>();

        public int Count => _resourses.Count;

        public bool IsFull => _maxAmount > 0 && Count >= _maxAmount;

        public ResourseId ResourseID => new ResourseId(_fixedResourse);

        public event UnityAction<Resourse> Added;
        public event UnityAction<Resourse> Removed;


        public bool TryAdd(Resourse resourse)
        {
            if (resourse.ID.Equals(new ResourseId(_fixedResourse)) == false)
                return false;
            if (IsFull)
                return false;
            _resourses.Add(resourse);
            Added?.Invoke(resourse);
            return true;
        }

        public Resourse TryRemove(ResourseId id)
        {
            if (_resourses.Count == 0)
                return null;
            for(int i = _resourses.Count-1; i>=0;i--)
            {
                Resourse resourse = _resourses[i];
                if (resourse.ID.Equals(id))
                {
                    _resourses.Remove(resourse);
                    Removed?.Invoke(resourse);
                    return resourse;
                }
            }
            return null;
        }

        public void SetMax(int max)
        {
            if (max < 0)
                throw new System.ArgumentException();
            _maxAmount = max;
        }

        public bool Contains(int resourseId)
        {
            foreach (Resourse resourse in _resourses)
            {
                if (resourse.ID.Value == resourseId)
                    return true;
            }
            return false;
        }
    }
}