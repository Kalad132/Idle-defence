using UnityEngine;
using Stacking;

namespace Base
{
    public abstract class TransferZone : MonoBehaviour, ICarryerPriority
    {
        [SerializeField] private Bag _bag;

        public Bag Bag => _bag;

        public abstract Transfer CreateTransfer(Bag comerBag);

        public abstract int Priority();
    }
}