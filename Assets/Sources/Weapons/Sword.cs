using System;
using System.Collections;
using UnityEngine;

public class Sword : MonoBehaviour, IWeapon
{
    [SerializeField] private Animator _animator;
    [SerializeField] private PhysicBroadsaster _swordCollider;

    private Action _onEnd;
    private bool _isAttack;

    private void OnEnable()
    {
        _swordCollider.Collised += OnCollised;
    }

    private void OnDisable()
    {
        _swordCollider.Collised -= OnCollised;
    }

    public void Use()
    {
        throw new NotImplementedException();
    }

    public void StopUse()
    {
        throw new NotImplementedException();
    }

    public void Use(Action onEnd)
    {
        if (_isAttack)
            return;

        _isAttack = true;
        _animator.SetTrigger(AnimatorPaladinController.States.Attack);
        _onEnd = onEnd;

        StartCoroutine(OnCompleteAttackAnimation());
    }

    private IEnumerator OnCompleteAttackAnimation()
    {
        yield return null;

        while (_animator.IsInTransition(0) || _animator.GetCurrentAnimatorStateInfo(0).IsName(AnimatorPaladinController.States.Attack))
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