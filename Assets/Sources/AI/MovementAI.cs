using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MovementAI : MonoBehaviour
{
    [SerializeField] private CharacterControllerMovement _movement;
    [SerializeField] private NavMeshAgent _navMeshAgent;
    [SerializeField] private Transform _target;
    [SerializeField] private float _changeDistance;
    [SerializeField] private float _stopDistance;
    [SerializeField] private float _angularSpeed;

    private Vector3 _previousTargetPosition;
    private int _currentCornerIndex;

    private Vector3 _currentPositionAtPath => _navMeshAgent.path.corners[_currentCornerIndex];

    private bool _isLastCorner => _currentCornerIndex == _navMeshAgent.path.corners.Length - 1;

    private void Start()
    {
        _changeDistance *= _changeDistance;
        _stopDistance *= _stopDistance;
        _navMeshAgent.updatePosition = false;
        _navMeshAgent.updateRotation = false;
    }

    private void Update()
    {
        CalculatePath();
        MoveToTarget();

        _previousTargetPosition = _target.position;
    }

    private void CalculatePath()
    {
        if (Vector3.SqrMagnitude(_previousTargetPosition - transform.position) < _changeDistance)
            return;

        _navMeshAgent.SetDestination(_target.position);
        _currentCornerIndex = 0;
    }

    private void MoveToTarget()
    {
        if (_navMeshAgent.pathPending || _navMeshAgent.remainingDistance <= _navMeshAgent.stoppingDistance)
        {
            _movement.MoveForward(0);
            return;
        }

        if (Vector3.SqrMagnitude(transform.position - _currentPositionAtPath) <= _stopDistance)
        {
            if (_isLastCorner)
            {
                _movement.MoveForward(0);
                return;
            }

            NextCornerIndex();
        }

        RotateToTarget(_currentPositionAtPath);
        _movement.MoveForward(1);
        _navMeshAgent.nextPosition = transform.position;
    }

    private void RotateToTarget(Vector3 target)
    {
        Vector3 direction = (target - transform.position).normalized;
        Quaternion rotation = Quaternion.LookRotation(direction, Vector3.up);
        transform.rotation = Quaternion.Slerp(transform.rotation, rotation, _angularSpeed * Time.deltaTime);
    }

    private void NextCornerIndex()
    {
        if (_isLastCorner)
            return;

        _currentCornerIndex++;
    }
}