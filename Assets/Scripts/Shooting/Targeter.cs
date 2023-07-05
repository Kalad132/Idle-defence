using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Shooting
{
    public class Targeter : MonoBehaviour
    {

        [SerializeField] private float _maxDistance = 10;
        [SerializeField] private AllTargets _targets;
        [SerializeField] private Transform _body;
        [SerializeField] private bool _bossFocus;

        public Target CurrentTarget { get; private set; }

        public event UnityAction<Target> TargetChanged;

        private void Awake()
        {
            CurrentTarget = null;
            TargetChanged?.Invoke(CurrentTarget);
        }

        private void OnDisable()
        {
            CurrentTarget = null;
            TargetChanged?.Invoke(CurrentTarget);
        }

        private void Update()
        {
            if (CurrentTarget == null || CheckValid(CurrentTarget) == false)
                Select(FindNearest());
            else
                RotateToTarget();
        }

        public void UpdateTarget()
        {
            Select(FindNearest());
        }

        private Target FindNearest()
        {
            if (_targets.All.Count == 0)
                return null;
            float closestDistance = float.MaxValue;
            Target closest = _targets.All[0];
            foreach (Target target in _targets.All)
            {
                if (_bossFocus && target.IsBoss && CheckValid(target))
                {
                    return target;
                }
                float distance = Vector3.Distance(target.transform.position,transform.position);
                if (distance < closestDistance)
                {
                   closestDistance = distance;
                   closest = target;
                }
            }
            if (CheckValid(closest) == false)
                return null;
            return closest;
        }

        public List<Target> FindInCone(Vector3 direction, float angle)
        {
            if (_targets.All.Count == 0)
                return null;
            List<Target> targets = new List<Target>();
            foreach (Target target in _targets.All)
            {
                Vector3 targetDirection = target.Position - transform.position;
                if (Vector3.Angle(direction, targetDirection) < angle/2)
                    targets.Add(target);
            }
            return targets;
        }

        private void Select(Target target)
        {
            if (CurrentTarget != null)
                CurrentTarget.Died -= OnTargetDeath;
            CurrentTarget = target;
            TargetChanged?.Invoke(CurrentTarget);
            if (CurrentTarget != null)
                CurrentTarget.Died += OnTargetDeath;
        }

        private void OnTargetDeath()
        {
            if (CurrentTarget != null)
                CurrentTarget.Died -= OnTargetDeath;
            Select(FindNearest());
            TargetChanged?.Invoke(CurrentTarget);
        }

        private void RotateToTarget()
        {
            Vector3 direction = CurrentTarget.Position - transform.position;
            direction.y = 0;
            _body.rotation = Quaternion.LookRotation(direction);
        }

        private bool CheckValid(Target target)
        {
            if (Vector3.Distance(target.transform.position, transform.position) > _maxDistance)
                return false;
            return true;
        }
    }
}