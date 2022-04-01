using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackAIState : MonoBehaviour, IStateAI
{
    [SerializeField] private Transitions _transitions;
    [SerializeField] private Animator _animator;
    [SerializeField] private Health _target;
    [SerializeField] private float _damage;
    [SerializeField] private float _damageApplayDistance;
    [SerializeField] private float _damageApplayTime;

    public event Action Completed;

    private void OnEnable()
    {
        StartCoroutine(Attack());
    }

    public void Enable()
    {
        enabled = true;
        _transitions.Enable();
    }

    public void Disable()
    {
        enabled = false;
    }

    private IEnumerator Attack()
    {
        _animator.SetTrigger(AnimatorPaladinController.States.Attack);
        yield return new WaitForSeconds(_damageApplayTime);
        SendDamage();
        Completed?.Invoke();
    }

    private void SendDamage()
    {
        if (Vector3.Distance(transform.position, _target.transform.position) > _damageApplayDistance)
            return;

        _target.TakeDamage(_damage);
    }
}