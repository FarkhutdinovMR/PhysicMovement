using System;
using UnityEngine;

public class CharacterControllerMovement : MonoBehaviour, IMovement
{
    [SerializeField] private CharacterController _characterController;
    [SerializeField] private Transform _camera;
    [SerializeField] private float _walkSpeed;
    [SerializeField] private float _runSpeed;
    [SerializeField] private float _gravityScale;
    [SerializeField] private float _acceleration;
    [SerializeField] private float _rotateSpeed;

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

    public void Move(Vector2 direction)
    {
        LookToCameraDirection(direction);

        _desiredMove = new Vector3(transform.forward.x, _desiredMove.y, transform.forward.z) * direction.magnitude;

        ApplyGravity();
        ControlSpeed(direction);

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

    private void ControlSpeed(Vector2 direction)
    {
        float speed = _targetSpeed;

        if (direction == Vector2.zero)
            speed = 0;

        _currentSpeed = Mathf.MoveTowards(_currentSpeed, speed, _acceleration * Time.deltaTime);
        _desiredMove *= _currentSpeed * Time.deltaTime;

        SpeedChanged?.Invoke(_currentSpeed);
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