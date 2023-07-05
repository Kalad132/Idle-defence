using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using Stacking;

namespace Base.Carryers
{
    public class TakingState : CarrierState
    {
        [SerializeField] private List<CarryerTarget> _targets;
        [SerializeField] private NavMeshAgent _agent;
        [SerializeField] private Bag _bag;
        [SerializeField] private GivingState _givingState;

        private CarryerTarget _activeTarget;
        private Vector3 _defaultPosition;

        private void Awake()
        {
            _defaultPosition = transform.position;
        }

        private void OnDisable()
        {
            _activeTarget = null;
            if (_agent.isActiveAndEnabled)
                _agent.SetDestination(_defaultPosition);
        }

        private void Update()
        {
            if (_activeTarget == null || _activeTarget.Bag.Count == 0)
            {
                if (_bag.Count > 0)
                    SwitchTo(_givingState);
                else
                    SwitchTo(this);
            }
            else if (_bag.IsFull)
            {
                SwitchTo(_givingState);
            }
        }
        protected override void OnEnter()
        {
            CarryerTarget target = FindTarget();
            SetTarget(target);
        }

        private void SetTarget(CarryerTarget target)
        {
            _activeTarget = target;
            Vector3 destination;
            if (_activeTarget == null)
                destination = _defaultPosition;
            else
                destination = _activeTarget.transform.position;
            if (destination != _agent.destination)
            {
                _agent.SetDestination(destination);
            }
        }

        private CarryerTarget FindTarget()
        {
            CarryerTarget result = null;
            foreach (CarryerTarget target in _targets)
                if (target.Priority > 0)
                    if (result == null || target.Priority > result.Priority)
                        result = target;
            return result;
        }
    }
}