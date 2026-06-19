using UnityEngine;

[RequireComponent(typeof(CharacterController))]
[RequireComponent(typeof(PlayerInput))]
public class PlayerMovement : MonoBehaviour
{
    private CharacterController _characterController;
    private PlayerInput _playerInput;

    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private float gravity = -9.81f;

    private Vector3 move;
    private Vector3 velocity;

    void Start()
    {
        _characterController = GetComponent<CharacterController>();
        _playerInput = GetComponent<PlayerInput>();
    }

    void Update()
    {
        // 入力（x,z）
        move.x = _playerInput.MovementInput.x;
        move.z = _playerInput.MovementInput.y;

        // 移動
        _characterController.Move(move * moveSpeed * Time.deltaTime);

        // 地面にいるなら落下速度をリセット
        if (_characterController.isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }

        // 重力
        velocity.y += gravity * Time.deltaTime;

        // 重力を反映
        _characterController.Move(velocity * Time.deltaTime);
    }
}
