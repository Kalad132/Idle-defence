using Player;
using UnityEngine;
using System.Collections;
using UnityEngine.AI;
using Shooting;

public class Aggression : MonoBehaviour
{
    [SerializeField] private NavMeshAgent _agent;
    [SerializeField] private Target _target;

    [SerializeField] private float _time;

    private Coroutine _chasing;
    private Vector3 _defaultPosition;
    private Transform _aggressionSource;

    public bool IsChasing => _chasing != null;

    private void OnEnable()
    {
        _target.Damaged += OnDamaged;
    }

    private void OnDisable()
    {
        _target.Damaged -= OnDamaged;
    }

    private void Start()
    {
        _defaultPosition = transform.position;
    }

    private void Update()
    {
        if (_aggressionSource == null)
            return;
        _agent.SetDestination(_aggressionSource.position);
    }

    private void OnDamaged(Damage damage)
    {
        if (damage.AggressionSource == null)
            return;
        StartChase(damage.AggressionSource);
    }

    private void StartChase(Transform aggressionSouece)
    {
        if (_chasing != null)
            StopCoroutine(_chasing);
        _chasing = StartCoroutine(Chase(aggressionSouece));
    }

    private IEnumerator Chase(Transform aggresionSource)
    {
        _aggressionSource = aggresionSource;
        yield return new WaitForSeconds(_time);
        _chasing = null;
        _aggressionSource = null;
        _agent.SetDestination(_defaultPosition);
    }

}
