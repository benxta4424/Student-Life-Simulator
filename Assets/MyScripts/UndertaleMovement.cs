using UnityEngine;

public class UndertaleMovement : MonoBehaviour
{
    [Header("Setări de Mișcare")]
    [Tooltip("Viteza de deplasare a jucătorului.")]
    [SerializeField] private float moveSpeed = 45f; 
    
    // Referințe la componente
    private Rigidbody2D rb;
    private Animator animator; 
    private Vector2 movementInput;
    
    // Ultima direcție pentru orientarea Idle
    private Vector2 lastDirection = Vector2.down; 

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>(); 

        if (rb == null)
        {
            Debug.LogError("UndertaleMovement necesită componenta Rigidbody2D!");
        }
        
        // Asigură-te că rotația pe Z este blocată
        rb.freezeRotation = true;
        
        // Asigură-te că jucătorul are Tag-ul "Player"
        if (!gameObject.CompareTag("Player"))
        {
            Debug.LogWarning("Obiectul jucătorului nu are Tag-ul 'Player'!");
        }
    }

    void Update()
    {
        // 1. Input
        movementInput.x = Input.GetAxisRaw("Horizontal");
        movementInput.y = Input.GetAxisRaw("Vertical");
        
        // 2. ROTEȘTE CARACTERUL (FLIP)
        FlipCharacter(movementInput.x); 

        // Normalizează vectorul
        if (movementInput.sqrMagnitude > 1) 
        {
            movementInput.Normalize();
        }

        // 3. ACTUALIZEAZĂ ULTIMA DIRECȚIE (pentru animația Idle)
        if (movementInput.sqrMagnitude > 0.01f)
        {
            if (Mathf.Abs(movementInput.x) > Mathf.Abs(movementInput.y))
            {
                lastDirection.x = movementInput.x > 0 ? 1f : -1f; 
                lastDirection.y = 0f;
            }
            else
            {
                lastDirection.x = 0f;
                lastDirection.y = movementInput.y > 0 ? 1f : -1f; 
            }
        }

        // 4. Animații
        UpdateAnimations();
    }
    
    void FixedUpdate()
    {
        // Aplică mișcarea
        rb.linearVelocity = movementInput * moveSpeed;
    }
    
    private void UpdateAnimations()
    {
        if (animator == null) return;
        
        bool isMoving = movementInput.x != 0 || movementInput.y != 0;
        animator.SetBool("IsMoving", isMoving);
        
        Vector2 currentDir = isMoving ? movementInput : lastDirection;
        animator.SetFloat("MoveX", currentDir.x);
        animator.SetFloat("MoveY", currentDir.y);
    }
    
    // Funcție pentru a roti Sprite-ul prin inversarea scalei pe X
    private void FlipCharacter(float horizontalInput)
    {
        Vector3 currentScale = transform.localScale;
        float absScale = Mathf.Abs(currentScale.x); 
        
        if (horizontalInput > 0) // Dreapta
        {
            currentScale.x = -absScale; 
        }
        else if (horizontalInput < 0) // Stânga
        {
            currentScale.x = absScale; 
        }
        
        transform.localScale = currentScale;
    }
}