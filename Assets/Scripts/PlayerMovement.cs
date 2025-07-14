using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerMovement : MonoBehaviour
{
    public InputSystem_Actions Input { get; private set; }

    [Header("Movement")]
    [SerializeField] float speed = 3f;
    Rigidbody2D rb;
    Vector2 moveVector;

    [Header("Jump")]
    [SerializeField] float jumpForce = 15f;
    [SerializeField] float gravity = -9.81f;
    [SerializeField] LayerMask groundMask;
    bool grounded = false;

    private void OnEnable()
    {
        // Create input class
        Input = new InputSystem_Actions();

        Input.Player.Move.performed += OnMovePerformed;
        Input.Player.Move.canceled += OnMoveCanceled;
        Input.Player.Jump.started += OnJumpStarted;

        Input.Enable();
    }

    private void OnDisable()
    {
        Input.Player.Move.performed -= OnMovePerformed;
        Input.Player.Move.canceled -= OnMoveCanceled;
        Input.Player.Jump.started -= OnJumpStarted;

        Input.Disable();
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

        // scale the player sprite according to the moving direction
        SpriteScale(moveVector.x);
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
        // Apply movement vector
        rb.linearVelocityX = moveVector.x * speed * Time.fixedDeltaTime;

        // Additional gravity
        rb.AddForceY(gravity, ForceMode2D.Impulse);
    }

    private void SpriteScale(float xForce) 
    {
        if (xForce < 0)
        {
            transform.localScale = new Vector2(-1,1);
        }
        else
        {
            transform.localScale = new Vector2(1, 1);
        }
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
