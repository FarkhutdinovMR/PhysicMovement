using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChaserAIState : MonoBehaviour, IStateAI
{
    [SerializeField] private Transitions _transitions;
    [SerializeField] private Transform _target;
    [SerializeField] private MovementAI _movement;
    [SerializeField] private float _changeDistance;

    private Vector3 _previousTargetPosition;

    public event Action Completed;

    private void OnEnable()
    {
        _previousTargetPosition = transform.position;
        _movement.WayPassed += OnWayPassed;
        _transitions.Enable();
    }

    private void OnDisable()
    {
        _movement.WayPassed -= OnWayPassed;
    }

    private void Start()
    {
        _changeDistance *= _changeDistance;
    }

    private void Update()
    {
        CalculatePath();
        _movement.Move();
    }

    public void Enable()
    {
        enabled = true;
    }

    public void Disable()
    {
        enabled = false;
    }

    private void CalculatePath()
    {
        if (Vector3.SqrMagnitude(_previousTargetPosition - _target.position) < _changeDistance)
            return;

        _movement.SetDestination(_target.position);
        _previousTargetPosition = _target.position;
    }

    private void OnWayPassed()
    {
        Completed?.Invoke();
    }
}