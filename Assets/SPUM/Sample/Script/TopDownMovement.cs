using UnityEngine;

public class TopDownMovement : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 10f; // Using 10f from your Inspector image

    private Rigidbody2D rb;
    private Animator animator;
    private Vector2 movement;
    
    // Start is called once before the first execution of Update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        // 1. Get RAW Input
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");
        
        // 2. Flip the character's visual direction based on X input
        // This must happen BEFORE normalization or animation update
        FlipCharacter(movement.x);
        
        // 3. Update Animator (if applicable)
        if (animator != null)
        {
            if (movement.x != 0 || movement.y != 0)
            {
                animator.SetFloat("MoveX", movement.x);
                animator.SetFloat("MoveY", movement.y);
                animator.SetBool("IsMoving", true);
            }
            else
            {
                animator.SetBool("IsMoving", false);
            }
        }
        
        // 4. Normalize movement for consistent diagonal speed
        movement.Normalize();
    }
    
    // FixedUpdate is used for physics calculations
    void FixedUpdate()
    {
        // Use rb.velocity for Kinematic/Dynamic Rigidbody movement
        if (rb != null)
        {
            Vector2 desiredVelocity = movement * moveSpeed;
            rb.linearVelocity = desiredVelocity;
        }
    }


    private void FlipCharacter(float horizontalInput)
    {
        // Get the current scale of the character object (e.g., SPUM_2024...)
        Vector3 currentScale = transform.localScale;
        
        if (horizontalInput > 0) // Moving Right
        {
            // Set scale X to 1 (facing right, default orientation)
            currentScale.x = -4f;
        }
        else if (horizontalInput < 0) // Moving Left
        {
            // Set scale X to -1 (flipped orientation)
            currentScale.x = 4f;
        }

        // Apply the new scale back to the character's transform
        transform.localScale = currentScale;
    }
}