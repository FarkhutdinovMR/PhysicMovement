using UnityEngine;

public class GroundChecker : MonoBehaviour
{
    [SerializeField] private Vector3 _offset;
    [SerializeField] private float _radius;
    [SerializeField] private float _maxDistance;
    [SerializeField] private LayerMask _layerMask;

    private RaycastHit _hitInfo;
    private bool _isGrounded;

    public bool IsGrounded => _isGrounded;

    public Vector3 Normal => _isGrounded ? _hitInfo.normal : Vector3.up;

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position + _offset, _radius);
        Gizmos.DrawWireSphere(transform.position + _offset + (Vector3.down * _maxDistance), _radius);
    }

    private void FixedUpdate()
    {
        _isGrounded = Physics.SphereCast(transform.position + _offset, _radius, Vector3.down, out _hitInfo, _maxDistance, _layerMask, QueryTriggerInteraction.Ignore);
    }
}