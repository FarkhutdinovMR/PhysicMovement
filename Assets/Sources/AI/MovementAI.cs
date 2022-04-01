using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MovementAI : MonoBehaviour
{
    [SerializeField] private CharacterControllerMovement _movement;
    [SerializeField] private NavMeshAgent _navMeshAgent;
    [SerializeField] private float _stopDistance;
    [SerializeField] private float _angularSpeed;

    private const float MoveDelta = 1;
    private int _currentCornerIndex;

    public event Action WayPassed;

    private Vector3 _currentPositionAtPath => _navMeshAgent.path.corners[_currentCornerIndex];

    private bool _isLastCorner => _currentCornerIndex == _navMeshAgent.path.corners.Length - 1;

    private void Start()
    {
        _navMeshAgent.stoppingDistance = _stopDistance;
        _stopDistance *= _stopDistance;
        _navMeshAgent.updatePosition = false;
        _navMeshAgent.updateRotation = false;
    }

    public void Move()
    {
        MoveToTarget();
    }

    public void SetDestination(Vector3 position)
    {
        _navMeshAgent.SetDestination(position);
        _currentCornerIndex = 0;
    }

    private void MoveToTarget()
    {
        if (_navMeshAgent.pathPending)
            return;

        if (Vector3.SqrMagnitude(transform.position - _currentPositionAtPath) <= _stopDistance)
        {
            if (_isLastCorner)
            {
                WayPassed?.Invoke();
                return;
            }

            NextCornerIndex();
        }

        RotateToTarget(_currentPositionAtPath);
        _movement.MoveForward(MoveDelta);
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