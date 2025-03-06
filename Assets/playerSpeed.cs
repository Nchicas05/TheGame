using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovementXR : MonoBehaviour
{
    public CharacterController characterController;
    public InputActionProperty moveInput;

    public float speed = 2.5f;
    public float gravity = -9.81f;
    public float jumpHeight = 1.5f;

    private Vector3 velocity;
    private bool isGrounded;

    void Update()
    {
       
        isGrounded = characterController.isGrounded;
        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -2f; 
        }

        // Get movement input from XR Controller
        Vector2 inputAxis = moveInput.action.ReadValue<Vector2>();
        Vector3 moveDirection = transform.right * inputAxis.x + transform.forward * inputAxis.y;
        characterController.Move(moveDirection * speed * Time.deltaTime);
        velocity.y += gravity * Time.deltaTime;
        characterController.Move(velocity * Time.deltaTime);
    }
}
