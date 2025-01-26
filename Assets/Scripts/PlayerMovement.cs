using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody2D rb;
    private InputManager inputManager;
    private PlayerInventory playerInventory;

    [SerializeField] private Transform groundCheck;

    [Header("Movement Settings")]
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private float jumpForce = 10f;
    [SerializeField] private float maxJumpTime = 0.25f;
    [SerializeField] private float groundCheckRadius = 0.2f;


    private Vector2 moveDirection;
    private bool isGrounded;
    private bool jumpPressed;
    private bool isJumping;
    private float jumpTime;

    private Vector3 baseScale;
    private float speedMultiplier = 1f;

    public Sprite defaultSprite;

    [SerializeField] private bool doubleJumpAvailable;
    private bool isDoubleJumpActive;
    private bool canDoubleJump = true;
    // getter and setter for the isDoubleJumpActive
    public bool IsDoubleJumpActive
    {
        get => isDoubleJumpActive;
        set => isDoubleJumpActive = value;
    }

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        inputManager = GetComponent<InputManager>();
        playerInventory = GetComponent<PlayerInventory>();
        baseScale = transform.localScale;
        // for test:
        IsDoubleJumpActive = true;
    }


    void Update()
    {
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, LayerMask.GetMask("Ground"));
        if (isGrounded)   
        {
            canDoubleJump = true;
        }
        moveDirection = inputManager.MoveInput.normalized;
    }

    void FixedUpdate()
    {
        rb.linearVelocity = new Vector2(moveDirection.x * moveSpeed * speedMultiplier, rb.linearVelocity.y);
    }

    public void OnJump(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            if (isGrounded)
            {
                SoundManager.Instance.PlayJumpSfx();
                rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce);
            }
            // Double jump
            else if (!isGrounded && IsDoubleJumpActive && doubleJumpAvailable && canDoubleJump)
            {
                SoundManager.Instance.PlayJumpSfx();
                rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce);
                canDoubleJump = false; // Consume double jump
            }
        }

        if (context.canceled && rb.linearVelocity.y > 0)
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, rb.linearVelocity.y * 0.4f);
        }
    }

    private void OnDrawGizmosSelected()
    {
        if (groundCheck != null){
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(groundCheck.position, groundCheckRadius);
        }
        
    }

    private void DoubleSize()
    {
        // doublle the size of the player if touch the powerup but need to make the collider get bigger with too
        if (transform.localScale.Equals(baseScale))
        {
            transform.localScale *= 1.2f;
        }
            

    }

    private void ReduceSize()
    {
        transform.localScale = baseScale;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("ScaleIncreaser"))
        {
            DoubleSize();   
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Door"))
        {
            // try to open the door
            collision.transform.parent.GetComponent<Door>().TryOpen(playerInventory);
        }

        if (collision.CompareTag("NextLevel"))
        {
            UIController.Instance.SetLevelComplete();
        }
    }

    public void SetSpeedMultiplier(float multiplier)
    {
        speedMultiplier = multiplier;
    }

}
