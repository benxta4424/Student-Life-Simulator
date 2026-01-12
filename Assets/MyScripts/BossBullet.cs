using UnityEngine;

public class BossBullet : MonoBehaviour
{
    [Header("Setări de Mișcare")]
    public float speed = 15f;
    public int damage = 10; // Câtă viață scade din hpPlayer
    
    private Rigidbody2D rb;
    private Vector2 moveDirection;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        // 1. Configurăm Rigidbody-ul
        if (rb != null)
        {
            rb.gravityScale = 0;
            rb.collisionDetectionMode = CollisionDetectionMode2D.Continuous;
        }

        // 2. Găsim Jucătorul în scenă
        GameObject player = GameObject.FindGameObjectWithTag("Player");

        if (player != null)
        {
            // Calculăm direcția de la glonț către jucător
            moveDirection = (player.transform.position - transform.position).normalized;

            // 3. Aplicăm viteza
            rb.linearVelocity = moveDirection * speed;

            // 4. ROTAȚIE
            float angle = Mathf.Atan2(moveDirection.y, moveDirection.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(0, 0, angle - 90f);
        }
        else
        {
            rb.linearVelocity = Vector2.down * speed;
        }

        // 5. Autodistrugere după 5 secunde
        Destroy(gameObject, 5f);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Verificăm dacă obiectul lovit este Jucătorul
        if (collision.CompareTag("Player"))
        {
            // Căutăm scriptul PlayerHealth pe care l-ai creat anterior
            PlayerHealth healthScript = collision.GetComponent<PlayerHealth>();

            if (healthScript != null)
            {
                // Scădem viața folosind funcția ta de TakeDamage
                healthScript.TakeDamage(damage); 
                Debug.Log("Jucătorul a fost lovit și a primit damage!");
            }
            
            // Distrugem glonțul la impact
            Destroy(gameObject);
        }
    }
}