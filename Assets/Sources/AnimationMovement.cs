using System;
using UnityEngine;

public class AnimationMovement : MonoBehaviour
{
    [SerializeField] private Animator _animator;
    [SerializeField] private MonoBehaviour _iMovement;

    private IMovement _movement => (IMovement)_iMovement;

    private void OnValidate()
    {
        if (_iMovement is IMovement)
            return;

        Debug.Log(_iMovement.name + " not implement " + nameof(IMovement));
        _iMovement = null;
    }

    private void OnEnable()
    {
        _movement.SpeedChanged += OnMoved;
    }

    private void OnDisable()
    {
        _movement.SpeedChanged -= OnMoved;
    }

    private void OnMoved(float speed)
    {
        _animator.SetFloat(AnimatorPaladinController.Params.Speed, speed);
    }
}