using UnityEngine;
using Stacking;

namespace Base.Carryers
{
    public class CarryerTarget : MonoBehaviour
    {
        [SerializeField] private TransferZone _zone;

        public Bag Bag => _zone.Bag;
        public int Priority => _zone.Priority() * priorityModifier;

        private int priorityModifier => gameObject.activeInHierarchy ? 1 : 0;
    }
}