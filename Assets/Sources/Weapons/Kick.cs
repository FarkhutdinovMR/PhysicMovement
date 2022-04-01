using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Kick : MonoBehaviour, ISimpleAttack
{
    [SerializeField] private DefaultAttack _defaultAttack;
    [SerializeField] private Stamina _stamina;
    [SerializeField] private float _attackCost;

    public void Attack(Action onEnd)
    {
        if (_stamina.isEmty)
        {
            onEnd();
            return;
        }

        _defaultAttack.Attack(AnimatorPaladinController.States.Kick, onEnd);
        _stamina.Spend(_attackCost);
    }
}