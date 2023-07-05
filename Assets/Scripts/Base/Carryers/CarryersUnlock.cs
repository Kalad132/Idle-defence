using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Base
{
    public class CarryersUnlock : MonoBehaviour
    {
        [SerializeField] private List<GameObject> _unlockedobj;
        [SerializeField] private List<GameObject> _Lockedobj;
        [SerializeField] private List<Buyable> _conditions;

        private int _boughtCount;

        private void Start()
        {
            foreach (GameObject locked in _Lockedobj)
            {
                locked.gameObject.SetActive(true);
            }
            foreach (GameObject locked in _unlockedobj)
            {
                locked.gameObject.SetActive(false);
            }
        }

        private void OnEnable()
        {
            foreach (Buyable buyable in _conditions)
            {
                buyable.Bought += OnBought;
            }
        }

        private void OnDisable()
        {
            foreach (Buyable buyable in _conditions)
            {
                buyable.Bought -= OnBought;
            }
        }

        private void OnBought()
        {
            _boughtCount++;
            if (_boughtCount == _conditions.Count)
                Unlock();
        }

        private void Unlock()
        {
            foreach (GameObject locked in _Lockedobj)
            {
                locked.gameObject.SetActive(false) ;
            }
            foreach (GameObject locked in _unlockedobj)
            {
                locked.gameObject.SetActive(true);
            }
        }
    }
}