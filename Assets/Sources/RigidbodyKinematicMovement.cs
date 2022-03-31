using System;
using UnityEngine;

public class RigidbodyKinematicMovement : MonoBehaviour, IMovement
{
    [SerializeField] private Rigidbody _rigidbody;
    [SerializeField] private GroundChecker _groundChecker;
    [SerializeField] private float _speed;
    [SerializeField] private float _jumpForce;
    [SerializeField] private float _gravityScale;
    [SerializeField] private float _drag;

    private Vector3 _desiredMove;

    public event Action<float> SpeedChanged;

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawRay(transform.position, _desiredMove);
    }

    public void Move(Vector2 direction)
    {
        Vector3 forward = transform.forward * direction.y;
        Vector3 sideways = transform.right * direction.x;
        _desiredMove = forward + sideways;

        _desiredMove = Vector3.ProjectOnPlane(_desiredMove, _groundChecker.Normal).normalized;

        _desiredMove *= _speed;

        if (_groundChecker.IsGrounded == false)
            _desiredMove += Physics.gravity * _gravityScale * Time.fixedDeltaTime;

        _rigidbody.position += _desiredMove * Time.fixedDeltaTime;

        SpeedChanged?.Invoke(_rigidbody.velocity.magnitude);
    }

    public void Jump()
    {
        if (_groundChecker.IsGrounded)
            _rigidbody.AddForce(Vector3.up * _jumpForce, ForceMode.Impulse);
    }

    public void Run()
    {
        throw new NotImplementedException();
    }

    public void StopRun()
    {
        throw new NotImplementedException();
    }
}