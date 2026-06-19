using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
public class PlayerInput : MonoBehaviour
{
    private PlayerInputAction _inputaction;
     public Vector2 MovementInput { get; private set; }

    private void Awake()
    {
        _inputaction = new PlayerInputAction();
        _inputaction.Charactar.Move.started += OnMovementInput;
        _inputaction.Charactar.Move.performed += OnMovementInput;
        _inputaction.Charactar.Move.canceled += OnMovementInput;

    }
     private void OnEnable()
    {
        _inputaction.Charactar.Enable();
    }
    private void OnDisable()
    {
        _inputaction.Charactar.Disable();
    }


    private void OnMovementInput(InputAction.CallbackContext context)
    {
        MovementInput = context.ReadValue<Vector2>();

        Debug.Log("Movement Input: " + MovementInput);
    }
}
