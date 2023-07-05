using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace Enemies
{
    public class EnemyMover : MonoBehaviour
    {
        [SerializeField] private NavMeshAgent _agent;
        [SerializeField] private float _standTime;

        private List<Area> _walkableAreas;
        private float _standTimer;

        public bool Standing => _agent.velocity.magnitude < 0.5f;

        private void Update()
        {
            if (Standing && _standTime > 0)
            {
                if (_standTimer > _standTime)
                {
                    MoveToRandomPoint();
                    _standTimer = 0;
                }
                else
                {
                    _standTimer += Time.deltaTime;
                }
            }
        }

        public void Init(List<Area> areas)
        {
            _walkableAreas = areas;
        }

        private void MoveToRandomPoint()
        {
            int randomIndex = Random.Range(0, _walkableAreas.Count);
            Area area = _walkableAreas[randomIndex];
            _agent.SetDestination(area.RandomPoint);
        }
    }
}