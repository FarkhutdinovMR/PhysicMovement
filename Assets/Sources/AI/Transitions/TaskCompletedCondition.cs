using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TaskCompletedCondition : MonoBehaviour, IConditionAI
{
    [SerializeField] private MonoBehaviour _stateInteface;

    private IStateAI _state => (IStateAI)_stateInteface;
    private bool _isDone;

    public bool IsDone => _isDone;

    private void OnEnable()
    {
        _state.Completed += OnCompleted;
    }

    private void OnDisable()
    {
        _state.Completed -= OnCompleted;
        _isDone = false;
    }

    public void Enable()
    {
        enabled = true;
    }

    public void Disable()
    {
        enabled = false;
    }

    private void OnCompleted()
    {
        _isDone = true;
    }
}