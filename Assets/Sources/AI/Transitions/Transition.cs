using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IStateAI
{
    public event Action Completed;

    public void Enable();

    public void Disable();
}

public interface IConditionAI
{
    public bool IsDone { get; }

    public void Enable();

    public void Disable();
}

public class Transition : MonoBehaviour
{
    [SerializeField] private MonoBehaviour _sourceInterface;
    [SerializeField] private MonoBehaviour _destanyInterface;
    [SerializeField] private MonoBehaviour _conditionInterface;

    private IStateAI _source => (IStateAI)_sourceInterface;
    private IStateAI _destany => (IStateAI)_destanyInterface;
    private IConditionAI _condition => (IConditionAI)_conditionInterface;

    private void OnEnable()
    {
        _condition.Enable();
    }

    private void Update()
    {
        if (_condition.IsDone == false)
            return;

        Transit();
        enabled = false;
    }

    private void Transit()
    {
        _source.Disable();
        _destany.Enable();
        _condition.Disable();
    }
}