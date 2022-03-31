using System;
using UnityEngine;

public class Shield : MonoBehaviour, IBlock
{
    [SerializeField] private Animator _animator;

    private bool _isBlock;

    public void Use()
    {
        _isBlock = true;
        _animator.SetBool(AnimatorPaladinController.States.Block, _isBlock);
    }

    public void StopUse()
    {
        _isBlock = false;
        _animator.SetBool(AnimatorPaladinController.States.Block, _isBlock);
    }
}