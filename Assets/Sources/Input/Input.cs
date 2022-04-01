using System;
using UnityEngine;
using UnityEngine.InputSystem;

public partial class Input : MonoBehaviour
{
    [SerializeField] private MonoBehaviour _iMovement;
    [SerializeField] private MonoBehaviour _iShield;
    [SerializeField] private Sword _rightHandWeapon;
    [SerializeField] private ThirdPersonCamera _thirdPersonCamera;
    [SerializeField] private Kick _kick;
    [SerializeField] private Transform _camera;
    [SerializeField] private float _rotateSpeed;

    private IMovement _movement => (IMovement)_iMovement;
    private IBlock _shield => (IBlock)_iShield;
    private CharacterInput _input;
    private bool _isAttack;
    private bool _isRun;

    public CharacterInput CharacterInput => _input;

    private void OnValidate()
    {
        if (_iMovement is IMovement && _iShield is IBlock)
            return;

        Debug.Log(_iMovement.name + " not implement " + nameof(IMovement));
        _iMovement = null;
    }

    private void OnEnable()
    {
        _input = new CharacterInput();
        _input.Enable();
        _input.Character.Look.performed += OnLookPerformed;
        _input.Character.Jump.performed += OnJumpPerformed;
        _input.Character.Run.performed += OnRunPerformed;
        _input.Character.Run.canceled += OnRunCanceled;
        _input.Character.Block.performed += OnBlockPerformed;
        _input.Character.Block.canceled += OnBlockCanceled;
        _input.Character.Attack.performed += OnAttackPerformed;
        _input.Character.Kick.performed += OnKickPerformed;
    }

    private void OnDisable()
    {
        _input.Character.Look.performed -= OnLookPerformed;
        _input.Character.Jump.performed -= OnJumpPerformed;
        _input.Character.Run.performed -= OnRunPerformed;
        _input.Character.Run.canceled -= OnRunCanceled;
        _input.Character.Block.performed -= OnBlockPerformed;
        _input.Character.Block.canceled -= OnBlockCanceled;
        _input.Character.Attack.performed -= OnAttackPerformed;
        _input.Character.Kick.performed -= OnKickPerformed;
        _input.Disable();
    }

    private void Update()
    {
        Vector2 direction;

        if (_isAttack)
            direction = Vector2.zero;
        else
            direction = _input.Character.Move.ReadValue<Vector2>();

        LookToCameraDirection(direction);
        _movement.MoveForward(direction.magnitude);
    }

    private void OnLookPerformed(InputAction.CallbackContext context)
    {
        Vector2 rawInput = context.ReadValue<Vector2>();
        Vector2 direction = new Vector2(-rawInput.y, rawInput.x);
        _thirdPersonCamera.Look(direction);
    }

    private void OnJumpPerformed(InputAction.CallbackContext context)
    {
        _movement.Jump();
    }

    private void OnRunPerformed(InputAction.CallbackContext context)
    {
        _isRun = true;
        _movement.Run();
    }

    private void OnRunCanceled(InputAction.CallbackContext context)
    {
        _isRun = false;
        _movement.StopRun();
    }

    private void OnBlockPerformed(InputAction.CallbackContext context)
    {
        _shield.Use();
    }

    private void OnBlockCanceled(InputAction.CallbackContext context)
    {
        _shield.StopUse();
    }

    private void OnAttackPerformed(InputAction.CallbackContext context)
    {
        if (_isAttack)
            return;

        _isAttack = true;

        if (_isRun)
            _rightHandWeapon.AttackInRunning(OnAttackEnded);
        else
            _rightHandWeapon.Attack(OnAttackEnded);
    }

    private void OnKickPerformed(InputAction.CallbackContext context)
    {
        if (_isAttack)
            return;

        _isAttack = true;
        _kick.Attack(OnAttackEnded);
    }

    private void OnAttackEnded()
    {
        _isAttack = false;
    }

    private void LookToCameraDirection(Vector2 direction)
    {
        if (direction == Vector2.zero)
            return;

        Vector3 forward = _camera.forward * direction.y + _camera.right * direction.x;
        forward = new Vector3(forward.x, 0, forward.z);
        Quaternion target = Quaternion.LookRotation(forward);
        transform.rotation = Quaternion.Slerp(transform.rotation, target, _rotateSpeed * Time.deltaTime);
    }
}