using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class AlongSurface : MonoBehaviour
{
    [SerializeField] private GroundChecker _groundChecker;
    [SerializeField] private int _maxNormalsCount = 5;

    private Queue<Vector3> _normals = new Queue<Vector3>();
    private Vector3 _previousPosition;

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawRay(transform.position, _groundChecker.Normal * 2);
    }

    private void FixedUpdate()
    {
        Vector3 averageNormal = CalculateAverageNormal();

        transform.rotation = Quaternion.FromToRotation(Vector3.up, averageNormal);

        _previousPosition = transform.position;
    }

    private Vector3 CalculateAverageNormal()
    {
        if (Equals(_previousPosition, transform.position) == false)
        {
            if (_normals.Count >= _maxNormalsCount)
                _normals.Dequeue();

            _normals.Enqueue(_groundChecker.Normal);
        }

        return  _normals.Aggregate(Vector3.zero, (acc, v) => acc + v) / _normals.Count;
    }
}