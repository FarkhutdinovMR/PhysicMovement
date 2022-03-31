using UnityEngine;

public class ThirdPersonCamera : MonoBehaviour
{
    [SerializeField] private Transform _target;
    [SerializeField] private Vector3 _offset;
    [SerializeField] private float _sensitivity;
    [SerializeField] private float _minVerticalAnlge;
    [SerializeField] private float _maxVerticalAngle;

    private void Start()
    {
        DisableCursor();
    }

    private void Update()
    {
        transform.position = _target.position + _offset;
    }

    public void Look(Vector2 direction)
    {
        Vector2 input = direction * _sensitivity;
        Quaternion rotation = transform.rotation * Quaternion.Euler(input.x, input.y, 0);
        rotation = ClampRotationAroundXAxis(rotation);
        rotation.eulerAngles = new Vector3(rotation.eulerAngles.x, rotation.eulerAngles.y, 0);
        transform.rotation = rotation;
    }

    private Quaternion ClampRotationAroundXAxis(Quaternion value)
    {
        value.x /= value.w;
        value.y /= value.w;
        value.z /= value.w;
        value.w = 1.0f;

        float angleX = 2.0f * Mathf.Rad2Deg * Mathf.Atan(value.x);

        angleX = Mathf.Clamp(angleX, _minVerticalAnlge, _maxVerticalAngle);

        value.x = Mathf.Tan(0.5f * Mathf.Deg2Rad * angleX);

        return value;
    }

    private void EnableCursor()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    private void DisableCursor()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }
}