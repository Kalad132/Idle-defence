using UnityEngine;
using System.Collections.Generic;
using Base;

namespace Stacking
{
    public class ResourseTransferer : MonoBehaviour
    {
        private List<Transfer> _transfers;

        private Vector3 _lastPosition;

        public IReadOnlyList<Transfer> Transfers => _transfers;

        private void Awake()
        {
            _transfers = new List<Transfer>();
        }

        private void Update()
        {
            if (transform.position == _lastPosition)
                TickAll();
            _lastPosition = transform.position;
        }

        public void Add(Transfer transfer)
        {
            _transfers.Add(transfer);
        }

        public void Remove(Transfer transfer)
        {
            _transfers.Remove(transfer);
        }

        private void TickAll()
        {
            List<Transfer> toRemove = new List<Transfer>();
            foreach (Transfer transfer in _transfers)
                if (transfer.Active)
                    transfer.Tick();
                else
                    toRemove.Add(transfer);
            foreach (Transfer transfer in toRemove)
                _transfers.Remove(transfer);
        }
    }
}