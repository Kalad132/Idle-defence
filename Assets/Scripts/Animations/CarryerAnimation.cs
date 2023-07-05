using UnityEngine;
using UnityEngine.AI;

namespace Animations
{
    public class CarryerAnimation : MonoBehaviour
    {
        [SerializeField] private Animator _animator;
        [SerializeField] private NavMeshAgent _agent;

        public bool Moving => _agent.velocity.magnitude > 0.5f;

        private void OnEnable()
        {
            if (_agent == null)
            {
                _animator.SetBool(Parameters.Standing, true);
            }
        }

        private void Update()
        {
            if (_agent == null)
                return;
            _animator.SetBool(Parameters.Standing, Moving == false);
        }

        public class Parameters
        {
            public static string Standing = nameof(Standing);
        }
    }
}