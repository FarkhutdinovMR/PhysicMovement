using UnityEngine;

public class Input : MonoBehaviour
{
    [SerializeField] private Movement _movement;

    private CharacterInput _input;
    private Vector2 _moveAxis;

    public CharacterInput CharacterInput => _input;

    private void OnEnable()
    {
        _input = new CharacterInput();
        _input.Enable();
    }

    private void OnDisable()
    {
        _input.Disable();
    }

    private void Update()
    {
        _moveAxis = _input.Character.Move.ReadValue<Vector2>();
    }

    private void FixedUpdate()
    {
        OnMove(_moveAxis);
    }

    private void OnMove(Vector2 axis)
    {
        Vector3 direction = new Vector3(axis.x, 0, axis.y);
        _movement.Move(direction);
    }
}