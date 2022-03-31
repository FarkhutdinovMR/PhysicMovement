using System;
using UnityEngine;
using UnityEngine.InputSystem;

public partial class Input : MonoBehaviour
{
    [SerializeField] private MonoBehaviour _iMovement;
    [SerializeField] private MonoBehaviour _iShield;
    [SerializeField] private MonoBehaviour _iSword;
    [SerializeField] private ThirdPersonCamera _thirdPersonCamera;

    private IMovement _movement => (IMovement)_iMovement;
    private IWeapon _shield => (IWeapon)_iShield;
    private IWeapon _sword => (IWeapon)_iSword;
    private CharacterInput _input;
    private bool _isAttack;

    public CharacterInput CharacterInput => _input;

    private void OnValidate()
    {
        if (_iMovement is IMovement && _iShield is IWeapon && _sword is IWeapon)
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
    }

    private void OnDisable()
    {
        _input.Character.Look.performed -= OnLookPerformed;
        _input.Character.Jump.performed -= OnJumpPerformed;
        _input.Character.Run.performed -= OnRunPerformed;
        _input.Character.Run.canceled -= OnRunCanceled;
        _input.Character.Block.performed -= OnBlockPerformed;
        _input.Character.Block.canceled -= OnBlockCanceled;
        _input.Disable();
    }

    private void Update()
    {
        if (_isAttack)
            _movement.Move(Vector2.zero);
        else
            _movement.Move(_input.Character.Move.ReadValue<Vector2>());
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
        _movement.Run();
    }

    private void OnRunCanceled(InputAction.CallbackContext context)
    {
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
        _isAttack = true;
        _sword.Use(OnAttackEnded);
    }

    private void OnAttackEnded()
    {
        _isAttack = false;
    }
}