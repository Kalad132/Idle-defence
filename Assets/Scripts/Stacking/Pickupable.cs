using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Stacking
{
    public class Pickupable : MonoBehaviour
    {
        [SerializeField] private Resourse _resourse;
        [SerializeField] private Rigidbody _body;
        [SerializeField] private BoxCollider _bodyCollider;
        [SerializeField] private BoxCollider _pickupcollider;

        public Resourse Resourse => _resourse;

        private void Awake()
        {
            if (_resourse != null)
                Init(_resourse);
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent(out Bag bag))
            {
                if (bag.TryAdd(_resourse))
                {
                    Destroy(_body.gameObject);
                }
            }
        }

        public void Init(Resourse resourse, float pickupDelay = 0)
        {
            if (_resourse != null && _resourse != resourse)
                Destroy(_resourse.gameObject);
            _resourse = resourse;
            resourse.transform.parent = transform;
            _bodyCollider.size = _resourse.Size;
            _pickupcollider.enabled = false;
            Invoke(nameof(TurnOn), pickupDelay);
        }

        private void TurnOn()
        {
            _pickupcollider.enabled = true;
        }
    }
}