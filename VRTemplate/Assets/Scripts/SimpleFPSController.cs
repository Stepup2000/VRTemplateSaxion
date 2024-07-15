using UnityEngine;

// Ensure the GameObject has a CharacterController component attached
[RequireComponent(typeof(CharacterController))]
public class SimpleFPSController : MonoBehaviour
{
    [Tooltip("Movement speed of the player.")]
    [SerializeField] private float speed = 5.0f;

    [Tooltip("Speed of the mouse look rotation.")]
    [SerializeField] private float lookSpeed = 2.0f;

    [Tooltip("Initial upward speed when jumping.")]
    [SerializeField] private float jumpSpeed = 8.0f;

    [Tooltip("The force of gravity applied to the player.")]
    [SerializeField] private float gravity = 20.0f;

    private CharacterController characterController; // Reference to the CharacterController component
    private Camera targetCamera; // Reference to the camera used for viewing
    private Vector3 moveDirection = Vector3.zero; // Direction the player is moving
    private float rotationX = 0.0f; // X rotation for the camera (up/down)
    private float rotationY = 0.0f; // Y rotation for the player (left/right)

    void Start()
    {
        // Get the CharacterController component attached to this GameObject
        characterController = GetComponent<CharacterController>();

        // Lock the cursor to the center of the screen for a better FPS experience
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        // Set the camera for the player
        SetCamera();
    }

    private void SetCamera()
    {
        // Find all cameras in the scene and assign the first one to targetCamera
        Camera[] cameras = FindObjectsOfType<Camera>();
        if (cameras.Length > 0)
        {
            targetCamera = cameras[0];
        }
        else
        {
            Debug.LogError("No camera found in the scene."); // Log an error if no camera is found
        }
    }

    void Update()
    {
        // Check if the target camera is set
        if (targetCamera != null)
        {
            // Handle player rotation based on mouse input
            rotationX += Input.GetAxis("Mouse X") * lookSpeed; // Rotate left/right
            rotationY -= Input.GetAxis("Mouse Y") * lookSpeed; // Rotate up/down
            rotationY = Mathf.Clamp(rotationY, -90, 90); // Clamp the vertical rotation to avoid flipping

            // Apply the rotations to the player and camera
            transform.localRotation = Quaternion.Euler(0, rotationX, 0);
            targetCamera.transform.localRotation = Quaternion.Euler(rotationY, 0, 0);

            // Handle player movement
            if (characterController.isGrounded) // Check if the player is grounded
            {
                // Get movement input from the user
                moveDirection = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
                moveDirection = transform.TransformDirection(moveDirection); // Convert to world space
                moveDirection *= speed; // Apply speed to the movement vector

                // Handle jumping
                if (Input.GetButton("Jump"))
                {
                    moveDirection.y = jumpSpeed; // Apply jump speed to the vertical movement
                }
            }

            // Apply gravity to the player
            moveDirection.y -= gravity * Time.deltaTime; // Update vertical movement with gravity
            characterController.Move(moveDirection * Time.deltaTime); // Move the player
        }
    }
}
