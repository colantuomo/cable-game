using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class PlayerController : MonoBehaviour
{
    public float speed = 6.0f; // Movement speed
    public float turnSpeed = 10.0f; // Turning speed
    public float jumpHeight = 2.0f; // Jump height
    public float gravity = -9.81f; // Gravity

    private CharacterController characterController;
    private Vector3 velocity;
    private bool isGrounded;
    private Vector3 moveDirection;

    public Transform cameraTransform; // Reference to the camera's transform

    private Transform currentPlatform;
    private Vector3 platformPreviousPosition;

    private Animator _anim;

    void Start()
    {
        _anim = GetComponent<Animator>();
        characterController = GetComponent<CharacterController>();
        if (cameraTransform == null)
        {
            Debug.LogError("Camera Transform is not assigned.");
        }
    }

    void Update()
    {
        // Check if the player is grounded
        isGrounded = characterController.isGrounded;

        // Handle platform movement
        if (isGrounded && currentPlatform != null)
        {
            Vector3 platformMovement = currentPlatform.position - platformPreviousPosition;
            characterController.Move(platformMovement);
            platformPreviousPosition = currentPlatform.position;
        }

        // Reset the vertical velocity if grounded
        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }

        // Get input for moving along the horizontal and vertical axis
        float moveHorizontal = Input.GetAxisRaw("Horizontal");
        float moveVertical = Input.GetAxisRaw("Vertical");

        // Create a direction vector based on the camera's forward and right vectors
        Vector3 cameraForward = cameraTransform.forward;
        cameraForward.y = 0; // Ensure movement is only on the horizontal plane
        cameraForward.Normalize();

        Vector3 cameraRight = cameraTransform.right;
        cameraRight.y = 0; // Ensure movement is only on the horizontal plane
        cameraRight.Normalize();

        Vector3 direction = (cameraForward * moveVertical + cameraRight * moveHorizontal).normalized;

        // If there's any input
        if (direction.magnitude >= 0.1f)
        {
            _anim.Play("run");
            // Calculate the target angle in degrees
            float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg;

            // Smoothly rotate the player towards the target angle
            float angle = Mathf.LerpAngle(transform.eulerAngles.y, targetAngle, turnSpeed * Time.deltaTime);
            transform.rotation = Quaternion.Euler(0f, angle, 0f);

            // Move the player in the direction they are facing
            moveDirection = transform.forward * speed;
        }
        else if (isGrounded)
        {
            _anim.Play("idle");
            // Stop movement when there is no input and the player is grounded
            moveDirection = Vector3.zero;
        }

        // Handle jumping
        if ((Input.GetButtonDown("Fire1") || Input.GetKeyDown(KeyCode.Space)) && isGrounded)
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
        }

        // Apply gravity
        velocity.y += gravity * Time.deltaTime;

        // Combine moveDirection with velocity
        Vector3 combinedVelocity = moveDirection + new Vector3(0, velocity.y, 0);
        characterController.Move(combinedVelocity * Time.deltaTime);
    }

    void OnControllerColliderHit(ControllerColliderHit hit)
    {
        // Check if the player is on a platform
        if (hit.collider.CompareTag("Platform"))
        {
            if (currentPlatform == null)
            {
                currentPlatform = hit.collider.transform;
                platformPreviousPosition = currentPlatform.position;
            }
        }
        else
        {
            currentPlatform = null;
        }
    }
}
