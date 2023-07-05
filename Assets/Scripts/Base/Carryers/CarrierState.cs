using UnityEngine;

namespace Base.Carryers
{
    public abstract class CarrierState : MonoBehaviour
    {
        public void SwitchTo(CarrierState state)
        {
            enabled = false;
            state.enabled = true;
            state.OnEnter();
        }

        protected abstract void OnEnter();
    }
}