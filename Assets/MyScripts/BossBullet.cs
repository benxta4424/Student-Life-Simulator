using UnityEngine;

public class BossBullet : MonoBehaviour
{
    [Header("Setări de Mișcare")]
    public float speed = 15f;
    public int damage = 10; 
    
    private Rigidbody2D rb;
    private Vector2 moveDirection;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        if (rb != null)
        {
            rb.gravityScale = 0;
            rb.collisionDetectionMode = CollisionDetectionMode2D.Continuous;
        }

        // cauta player dupa tag
        GameObject player = GameObject.FindGameObjectWithTag("Player");

        if (player != null)
        {
            
            moveDirection = (player.transform.position - transform.position).normalized;

            
            rb.linearVelocity = moveDirection * speed;

            
            float angle = Mathf.Atan2(moveDirection.y, moveDirection.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(0, 0, angle - 90f);
        }
        else
        {
            rb.linearVelocity = Vector2.down * speed;
        }

    
        Destroy(gameObject, 5f);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Verificăm dacă obiectul lovit este Jucătorul
        if (collision.CompareTag("Player"))
        {
           
            PlayerHealth healthScript = collision.GetComponent<PlayerHealth>();

            if (healthScript != null)
            {
                // Scădem viața folosind funcția de TakeDamage
                healthScript.TakeDamage(damage); 
                Debug.Log("Jucătorul a fost lovit și a primit damage!");
            }
            
            // Distrugem glonțul la impact
            Destroy(gameObject);
        }
    }
}