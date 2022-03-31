using System;
using System.Collections;
using UnityEngine;

public abstract class DefaultAttack : MonoBehaviour
{
    [SerializeField] private Animator _animator;
    [SerializeField] private PhysicBroadsaster _collider;

    private Action _onEnd;
    private bool _isAttack;

    private void OnEnable()
    {
        _collider.Collised += OnCollised;
    }

    private void OnDisable()
    {
        _collider.Collised -= OnCollised;
    }

    protected void Attack(string animation, Action onEnd)
    {
        if (_isAttack)
            return;

        _isAttack = true;
        _animator.SetTrigger(animation);
        _animator.applyRootMotion = true;
        _onEnd = onEnd;

        StartCoroutine(OnCompleteAttackAnimation(animation));
    }

    private IEnumerator OnCompleteAttackAnimation(string animation)
    {
        yield return null;

        while (_animator.IsInTransition(0) || _animator.GetCurrentAnimatorStateInfo(0).IsName(animation))
            yield return null;

        _onEnd.Invoke();
        _isAttack = false;
    }

    private void OnCollised(GameObject obj)
    {
        if (_isAttack)
            TryPushObject(obj);
    }

    private void TryPushObject(GameObject obj)
    {
        if (obj.TryGetComponent(out Rigidbody rigidbody))
            rigidbody.AddForce(transform.forward * 5f, ForceMode.Impulse);
    }
}