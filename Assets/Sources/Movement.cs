using UnityEngine;

public class Movement : MonoBehaviour
{
    [SerializeField] private Rigidbody _rigidbody;
    [SerializeField] private float _speed;

    public void Move(Vector3 direction)
    {
        _rigidbody.velocity = direction * _speed;
    }
}