using UnityEngine;
using System.Collections;

public class UndertaleMovement : MonoBehaviour
{
    [Header("Setări de Mișcare")]
    [SerializeField] private float moveSpeed = 15f; 
    [SerializeField] private float acceleration = 100f;

    [Header("Dash Agresiv")]
    [SerializeField] private float dashBurstSpeed = 60f; 
    [SerializeField] private float dashDuration = 0.15f; 
    [SerializeField] private float dashCooldown = 0.5f;
    
    [Header("Referințe SPUM (Important)")]
    [Tooltip("Trage aici obiectul 'Body' sau 'Sprite' din interiorul personajului tău SPUM")]
    [SerializeField] private SpriteRenderer characterSprite; 

    private Rigidbody2D rb;
    private Animator animator;
    
    private Vector2 moveInput;
    private Vector2 lastDirection = Vector2.down;
    private bool isDashing;
    private bool canDash = true;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();

        // Dacă nu ai tras manual sprite-ul în Inspector, încercăm să îl găsim automat în copii
        if (characterSprite == null)
        {
            characterSprite = GetComponentInChildren<SpriteRenderer>();
        }

        if (rb != null) {
            rb.bodyType = RigidbodyType2D.Dynamic;
            rb.gravityScale = 0;
            rb.freezeRotation = true;
            rb.collisionDetectionMode = CollisionDetectionMode2D.Continuous;
        }
    }

    void Update()
    {
        if (isDashing) return;

        moveInput.x = Input.GetAxisRaw("Horizontal");
        moveInput.y = Input.GetAxisRaw("Vertical");

        if (moveInput.sqrMagnitude > 0.01f)
        {
            lastDirection = moveInput.normalized;
            FlipCharacter(moveInput.x);
        }

        if (Input.GetKeyDown(KeyCode.Space) && canDash)
        {
            StartCoroutine(PerformDash());
        }

        UpdateAnimations();
    }

    void FixedUpdate()
    {
        if (isDashing) return;

        Vector2 targetVelocity = moveInput.normalized * moveSpeed;
        rb.linearVelocity = Vector2.MoveTowards(rb.linearVelocity, targetVelocity, acceleration * Time.fixedDeltaTime);
    }

    private IEnumerator PerformDash()
    {
        canDash = false;
        isDashing = true;

        Vector2 dashDir = moveInput.sqrMagnitude > 0 ? moveInput.normalized : lastDirection;

        rb.linearVelocity = Vector2.zero; 
        rb.linearVelocity = dashDir * dashBurstSpeed;

        // Pornim Ghost Trail doar dacă avem un SpriteRenderer valid
        if (characterSprite != null)
        {
            StartCoroutine(GhostTrailRoutine());
        }

        yield return new WaitForSeconds(dashDuration);

        rb.linearVelocity = rb.linearVelocity * 0.2f; 
        isDashing = false;

        yield return new WaitForSeconds(dashCooldown);
        canDash = true;
    }

    private IEnumerator GhostTrailRoutine()
    {
        while (isDashing)
        {
            // Creăm obiectul fantomă
            GameObject ghost = new GameObject("DashGhost");
            ghost.transform.position = characterSprite.transform.position;
            ghost.transform.rotation = characterSprite.transform.rotation;
            ghost.transform.localScale = characterSprite.transform.lossyScale;

            SpriteRenderer gSr = ghost.AddComponent<SpriteRenderer>();
            
            // Copiem sprite-ul curent din SPUM
            gSr.sprite = characterSprite.sprite;
            gSr.color = new Color(1, 1, 1, 0.5f);
            gSr.sortingOrder = characterSprite.sortingOrder - 1;

            Destroy(ghost, 0.15f);
            yield return new WaitForSeconds(0.03f);
        }
    }

    private void UpdateAnimations()
    {
        if (animator == null) return;
        bool isMoving = moveInput.sqrMagnitude > 0.01f;
        animator.SetBool("IsMoving", isMoving);
        
        Vector2 animDir = isMoving ? moveInput : lastDirection;
        animator.SetFloat("MoveX", animDir.x);
        animator.SetFloat("MoveY", animDir.y);
    }

    private void FlipCharacter(float xInput)
    {
        // La SPUM, uneori e mai bine să dăm Flip la rădăcina vizuală
        if (xInput > 0) transform.localScale = new Vector3(-Mathf.Abs(transform.localScale.x), transform.localScale.y, 1);
        else if (xInput < 0) transform.localScale = new Vector3(Mathf.Abs(transform.localScale.x), transform.localScale.y, 1);
    }
}