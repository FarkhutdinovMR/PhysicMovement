using System;
using System.Collections;
using UnityEngine;

public class Sword : MonoBehaviour, ISimpleAttack, IRunningAttack
{
    [SerializeField] private DefaultAttack _simpleAttack;
    [SerializeField] private DefaultAttack _runningAttack;
    [SerializeField] private Stamina _stamina;
    [SerializeField] private float _simpleAttackCost;
    [SerializeField] private float _runningAttackCost;

    public void Attack(Action onEnd)
    {
        if (_stamina.isEmty)
        {
            onEnd();
            return;
        }

        _simpleAttack.Attack(AnimatorPaladinController.States.Attack, onEnd);
        _stamina.Spend(_simpleAttackCost);
    }

    public void AttackInRunning(Action onEnd)
    {
        if (_stamina.isEmty)
        {
            onEnd();
            return;
        }

        _runningAttack.Attack(AnimatorPaladinController.States.RunningAttack, onEnd);
        _stamina.Spend(_runningAttackCost);
    }    
}