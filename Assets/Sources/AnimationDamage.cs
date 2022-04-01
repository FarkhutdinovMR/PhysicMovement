using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationDamage : MonoBehaviour
{
    [SerializeField] private Animator _animator;
    [SerializeField] private Health _health;

    private void OnEnable()
    {
        _health.DamageRecieved += OnDamageRecieved;
        _health.Dies += OnDies;
    }

    private void OnDisable()
    {
        _health.DamageRecieved -= OnDamageRecieved;
        _health.Dies -= OnDies;
    }

    private void OnDamageRecieved()
    {
        _animator.SetTrigger(AnimatorPaladinController.States.DamageRecieved);
    }

    private void OnDies()
    {
        _animator.SetTrigger(AnimatorPaladinController.States.Dying);
    }
}