using UnityEngine;
using UnityEngine.Events;
using Player;

namespace Shooting
{
    [RequireComponent(typeof(Rigidbody))]
    public class ShootActivation : MonoBehaviour
    {
        [SerializeField] private Shooter _shooter;
        [SerializeField] private PlayerMover _mover;

        public event UnityAction<bool> StatusChanged;

        private void Start()
        {
            DeActivate();
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent(out ShootArea area))
                Activate();
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.TryGetComponent(out ShootArea area))
                DeActivate();
        }

        private void Activate()
        {
            _shooter.gameObject.SetActive(true);
            StatusChanged?.Invoke(true);
            _mover.FreeRotation = false;
        }

        private void DeActivate()
        {
            _shooter.gameObject.SetActive(false);
            StatusChanged?.Invoke(false);
            _mover.FreeRotation = true;
        }
    }
}