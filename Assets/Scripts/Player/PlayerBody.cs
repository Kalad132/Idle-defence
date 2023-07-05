using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Enemies;
using UnityEngine.Events;
using Shooting;

namespace Player
{
    public class PlayerBody : MonoBehaviour
    {
        [SerializeField] private BagDrop _bagDrop;
        [SerializeField] private PlayerDisabler _disabler;
        [SerializeField] private float _stunTime;
        [SerializeField] private Transform _view;

        private Coroutine _stunning;

        public event UnityAction<bool> Stunned;
        public event UnityAction<float> Hit;

        public void GetHit(Attacker enemy)
        {
            if (enabled == false)
                return;
            if (_stunning != null)
                return;
            _bagDrop.DropItems();
            Hit?.Invoke(enemy.Damage);
            Stun(transform.position - enemy.transform.position);
        }

        private void Stun(Vector3 direction)
        {
            if (_stunning != null)
            {
                StopCoroutine(_stunning);
                Stunned?.Invoke(false);
            }
            _stunning = StartCoroutine(PlayStun(direction));
        }

        private IEnumerator PlayStun(Vector3 Direction)
        {
            Stunned?.Invoke(true);
            _view.rotation = Quaternion.LookRotation(-Direction);
            _disabler.Disable(_stunTime);
            yield return new WaitForSeconds(_stunTime);
            Stunned?.Invoke(false);
            _stunning = null;
        }
    }
}