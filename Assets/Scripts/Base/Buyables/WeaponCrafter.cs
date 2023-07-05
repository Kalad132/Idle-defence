using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Stacking;

namespace Base
{
    public class WeaponCrafter : MonoBehaviour
    {
        [SerializeField] private Bag _bag;
        [SerializeField] private Belt _belt;
        [SerializeField] private float _craftSpeed;
        [SerializeField] private int _cost;
        [SerializeField] private Resourse _template;
        [SerializeField] private Press _press;
        [SerializeField] private float _pressDelay;

        private float timer;

        private void Update()
        {
            timer -= Time.deltaTime;
            if (timer <= 0)
            {
                timer = 1 / _craftSpeed;
                TryCraft();
            }
        }

        private void TryCraft()
        {
            if (_bag.Count < _cost)
                return;
            RemoveResourses();
            _press.MoveToBottom(_pressDelay);
            Invoke(nameof(Craft), _pressDelay);
        }

        private void RemoveResourses()
        {
            for (int i = 0; i < _cost; i++)
            {
                Resourse resouse = _bag.TryRemove(new ResourseId(2));
                Destroy(resouse.gameObject);
            }
        }

        private void Craft()
        {
            Resourse resourse = Instantiate(_template, transform.position, transform.rotation, transform);
            _belt.Transport(resourse);
            _press.MoveToTop(timer);
        }

    }
}