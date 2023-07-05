using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Stacking;

namespace Player
{
    public class BagDrop : MonoBehaviour
    {
        [SerializeField] private Bag _bag;
        [Range(0, 1)]
        [SerializeField] private float _dropAmount;
        [Range(0, 1)]
        [SerializeField] private float _destroyAmount;
        [SerializeField] private GameObject _pickupableTemplate;

        public void DropItems(bool all = false)
        {
            int dropAmount = Mathf.RoundToInt(_bag.Count * _dropAmount);
            if (all)
                dropAmount = _bag.Count;
            dropAmount = Mathf.Max(dropAmount, 1);
            for (int i=0; i< dropAmount; i++)
            {
                if (Random.Range(0f, 1f) <= _destroyAmount)
                    DestroyItem();
                else
                    DropItem();
            }
        }

        private void DropItem()
        {
            Resourse resourse = _bag.TryRemove(new ResourseId(0));
            if (resourse == null)
                return;
            GameObject dropedObj = Instantiate(_pickupableTemplate, resourse.transform.position, resourse.transform.rotation);
            Pickupable pickupable = dropedObj.GetComponentInChildren<Pickupable>();
            pickupable.Init(resourse, 3f);
            Rigidbody body = dropedObj.GetComponent<Rigidbody>();
            Vector3 direction = new Vector3(Random.Range(-1, 1), 5, Random.Range(-1, 1));
            float magnitude = 7;
            body.velocity = direction.normalized * magnitude;
        }

        private void DestroyItem()
        {
            Resourse resourse = _bag.TryRemove(new ResourseId(0));
            if (resourse == null)
                return;
            Destroy(resourse.gameObject);
        }
    }
}