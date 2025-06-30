using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerMovement : MonoBehaviour
{
    private InputSystem_Actions input;

    [Header("Movement")]
    [SerializeField] float speed = 3f;
    Rigidbody2D rb;
    Vector2 moveVector;
    Vector2 moveDirection;

    [Header("Jump")]
    [SerializeField] float jumpForce = 15f;
    [SerializeField] float gravity = -9.81f;
    [SerializeField] LayerMask groundMask;
    bool grounded = false;

    private void OnEnable()
    {
        // Create input class
        input = new InputSystem_Actions();

        input.Player.Move.performed += OnMovePerformed;
        input.Player.Move.canceled += OnMoveCanceled;
        input.Player.Jump.started += OnJumpStarted;

        input.Enable();
    }

    private void OnDisable()
    {
        input.Player.Move.performed -= OnMovePerformed;
        input.Player.Move.canceled -= OnMoveCanceled;
        input.Player.Jump.started -= OnJumpStarted;

        input.Disable();
    }

    private void Awake()
    {
        // Lock Cursor
        Cursor.lockState = CursorLockMode.Locked;

        // Get rigidbody2D component
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        GroundCheck();
    }

    private void FixedUpdate()
    {
        HandleMovement();
    }

    private void OnMovePerformed(InputAction.CallbackContext context)
    {
        moveVector = context.ReadValue<Vector2>();
    }

    private void OnMoveCanceled(InputAction.CallbackContext context) 
    {
        moveVector = Vector2.zero;    
    }

    private void OnJumpStarted(InputAction.CallbackContext context)
    {
        if (grounded)
        {
            rb.AddForceY(jumpForce, ForceMode2D.Impulse);
            Debug.Log("Jumped");
        }
    }

    private void HandleMovement() 
    {
        // Move force
        //moveDirection = new Vector2(, moveDirection.y);

        // Apply movement vector
        rb.linearVelocityX = moveVector.x * speed * Time.fixedDeltaTime;

        // Additional gravity
        rb.AddForceY(gravity, ForceMode2D.Impulse);
    }

    private void GroundCheck() 
    {
        if (Physics2D.Raycast(transform.position, Vector2.down, 1.1f, groundMask))
        {
            grounded = true;
        }
        else
        {
            if (grounded)
            {
                grounded = false;
            }

            grounded = false;
        }
        Debug.DrawRay(transform.position, Vector2.down * 1.1f, Color.green);
    }
}
