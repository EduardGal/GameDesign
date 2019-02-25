using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerMovement : MonoBehaviour {

    [SerializeField] float walkSpeed = 2.0f;
    [SerializeField] float runSpeed = 6.0f;
    [SerializeField] float gravity = -12.0f;
    [SerializeField] float rotationSmoothTime = 0.2f;
    [SerializeField] float speedSmoothTime = 0.05f;

    private float playerSpeed, animSpeedPercent, turnSmoothVelocity, speedSmoothVelocity, currentSpeed, velocityY;

    Animator animator;
    CharacterController characterController;
    Transform cameraTransform;

    void Start()
    {
        animator = GetComponent<Animator>();
        cameraTransform = Camera.main.transform;
        characterController = GetComponent<CharacterController>();
    }

    void Update()
    {
        // player inputs
        Vector2 playerInput = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        Vector2 inputDirection = playerInput.normalized;
        bool running = Input.GetKey(KeyCode.LeftShift);

        PlayerMovementAndRotation(inputDirection, running);

        // animations
        animSpeedPercent = ((running) ? currentSpeed / runSpeed : currentSpeed / walkSpeed * 0.5f);
        animator.SetFloat("speedPercent", animSpeedPercent, speedSmoothTime, Time.deltaTime);
    }

    void PlayerMovementAndRotation(Vector2 inputDirection, bool running)
    {
        if (inputDirection != Vector2.zero)
        {
            float targetRotation = Mathf.Atan2(inputDirection.x, inputDirection.y) * Mathf.Rad2Deg + cameraTransform.eulerAngles.y;
            transform.eulerAngles = Vector3.up * Mathf.SmoothDampAngle(transform.eulerAngles.y, targetRotation, ref turnSmoothVelocity, rotationSmoothTime);
        }

        playerSpeed = ((running) ? runSpeed : walkSpeed) * inputDirection.magnitude;
        currentSpeed = Mathf.SmoothDamp(currentSpeed, playerSpeed, ref speedSmoothVelocity, speedSmoothTime);

        velocityY += Time.deltaTime * gravity;
        Vector3 velocity = transform.forward * currentSpeed + Vector3.up * velocityY;

        characterController.Move(velocity * Time.deltaTime);
        currentSpeed = new Vector2(characterController.velocity.x, characterController.velocity.z).magnitude;

        if (characterController.isGrounded)
        {
            velocityY = 0;
        }
    }
}