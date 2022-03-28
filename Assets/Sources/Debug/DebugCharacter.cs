using UnityEngine;

public class DebugCharacter : MonoBehaviour
{
    [SerializeField] private TextView _velocity;
    [SerializeField] private Rigidbody _rigidbody;

    [SerializeField] private TextView _moveAxis;
    [SerializeField] private Input _input;

    private void Update()
    {
        _velocity.UpdateText(_rigidbody.velocity);
        _moveAxis.UpdateText(_input.CharacterInput.Character.Move.ReadValue<Vector2>());
    }
}