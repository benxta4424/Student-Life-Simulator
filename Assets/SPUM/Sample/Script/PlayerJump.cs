using UnityEngine;

public class PlayerJump : MonoBehaviour
{
    // === Inspector Variables ===
    [Header("Jump Settings")]
    [SerializeField] private float jumpForce = 8f; 
    
    [Header("Ground Detection")]
    [SerializeField] private Transform groundCheck;   // Position of the sensor (must be set in Inspector)
    [SerializeField] private LayerMask groundLayer;   // Which layer is considered ground (must be set in Inspector)
    [SerializeField] private float checkRadius = 0.2f; 

    // === Private References ===
    private Rigidbody2D rb;      
    private bool isGrounded;    

    void Awake()
    {
        // Get the Rigidbody2D component from this GameObject
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        // 1. Handle Ground Check in FixedUpdate (physics loop)
        // 2. Handle Jump Input in Update (input loop)
        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            Jump();
        }
    }

    void FixedUpdate()
    {
        // Perform the ground check using a circle that overlaps with the ground layer
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, checkRadius, groundLayer);
        
        // Optional: If you use an Animator, set the IsJumping or IsGrounded parameter here
    }

    private void Jump()
    {
        if (rb == null) return;

        // Apply force only if we are on the ground
        if (isGrounded)
        {
            // Set vertical velocity to zero before applying force for consistent jump height
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, 0f);

            // Apply force instantly (Impulse) in the upward direction
            rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
            
            // Set isGrounded to false to prevent mid-air spamming (will be reset by FixedUpdate collision check)
            isGrounded = false;
        }
    }

    // Optional: Draws the ground check circle in the editor for easy visualization
    private void OnDrawGizmosSelected()
    {
        if (groundCheck != null)
        {
            Gizmos.color = isGrounded ? Color.green : Color.red;
            Gizmos.DrawWireSphere(groundCheck.position, checkRadius);
        }
    }
}