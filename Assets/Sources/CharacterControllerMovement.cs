using System;
using UnityEngine;

public class CharacterControllerMovement : MonoBehaviour, IMovement
{
    [SerializeField] private Stamina _stamina;
    [SerializeField] private CharacterController _characterController;
    [SerializeField] private float _walkSpeed;
    [SerializeField] private float _runSpeed;
    [SerializeField] private float _gravityScale;
    [SerializeField] private float _acceleration;
    [SerializeField] private float _runCostInStamin;

    public event Action<float> SpeedChanged;

    private const float _groundGravity = -0.05f;
    private Vector3 _desiredMove;
    private float _currentSpeed;
    private float _targetSpeed;

    private void Start()
    {
        _targetSpeed = _walkSpeed;
        _desiredMove = transform.position;
    }

    public void MoveForward(float delta)
    {
        _desiredMove = new Vector3(transform.forward.x, _desiredMove.y, transform.forward.z) * delta;

        ApplyGravity();
        ControlSpeed(delta);

        _characterController.Move(_desiredMove);
    }

    public void Jump()
    {
    }

    public void Run()
    {
        _targetSpeed = _runSpeed;
    }

    public void StopRun()
    {
        _targetSpeed = _walkSpeed;
    }

    private void ApplyGravity()
    {
        if (_characterController.isGrounded)
            _desiredMove.y = _groundGravity;
        else
            _desiredMove += Physics.gravity * _gravityScale;
    }

    private void ControlSpeed(float delta)
    {
        float speed = _targetSpeed;

        if (speed == _runSpeed)
        {
            if (_stamina.isEmty)
                speed = _walkSpeed;
            else
                _stamina.Spend(_runCostInStamin * Time.deltaTime);
        }

        if (delta == 0)
            speed = 0;

        _currentSpeed = Mathf.MoveTowards(_currentSpeed, speed, _acceleration * Time.deltaTime);
        _desiredMove *= _currentSpeed * Time.deltaTime;

        SpeedChanged?.Invoke(_currentSpeed);
    }
}