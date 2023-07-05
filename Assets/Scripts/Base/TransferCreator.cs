using UnityEngine;
using System.Collections.Generic;
using Stacking;

namespace Base
{
    public class TransferCreator : MonoBehaviour
    {
        [SerializeField] private ResourseTransferer _transferer;
        [SerializeField] private Bag _selfBag;

        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent(out TransferZone zone))
            {
                Transfer transfer = zone.CreateTransfer(_selfBag);
                _transferer.Add(transfer);
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.TryGetComponent(out TransferZone zone))
            {
                foreach (Transfer transfer in _transferer.Transfers)
                {
                    if (transfer.Source == zone.Bag || transfer.Target == zone.Bag)
                    {
                        _transferer.Remove(transfer);
                        return;
                    }
                }
            }
        }
    }
}