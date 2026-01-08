using UnityEngine;

public class BossBullet : MonoBehaviour
{
    [Header("Setări de Mișcare")]
    public float speed = 15f; // Viteza poate fi reglată din Inspector
    
    private Rigidbody2D rb;
    private Vector2 moveDirection;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        // 1. Configurăm Rigidbody-ul să nu fie afectat de gravitate
        if (rb != null)
        {
            rb.gravityScale = 0;
            rb.collisionDetectionMode = CollisionDetectionMode2D.Continuous;
        }

        // 2. Găsim Jucătorul în scenă folosind Tag-ul "Player"
        GameObject player = GameObject.FindGameObjectWithTag("Player");

        if (player != null)
        {
            // Calculăm direcția de la glonț către jucător
            moveDirection = (player.transform.position - transform.position).normalized;

            // 3. Aplicăm viteza pe componenta de fizică
            rb.linearVelocity = moveDirection * speed;

            // 4. ROTAȚIE: Aliniem vârful sprite-ului cu direcția de mers
            // Atan2 calculează unghiul, iar Rad2Deg îl transformă în grade
            float angle = Mathf.Atan2(moveDirection.y, moveDirection.x) * Mathf.Rad2Deg;
            
            // Folosim -90f deoarece sprite-ul tău este desenat vertical
            transform.rotation = Quaternion.Euler(0, 0, angle - 90f);
        }
        else
        {
            // Dacă nu găsește jucătorul, merge pur și simplu în jos
            rb.linearVelocity = Vector2.down * speed;
        }

        // 5. Curățenie: Distrugem glonțul după 5 secunde să nu încarce memoria
        Destroy(gameObject, 5f);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Verificăm dacă obiectul lovit este Jucătorul
        if (collision.CompareTag("Player"))
        {
            Debug.Log("Jucătorul a fost lovit!");
            
            // Aici poți adăuga logica pentru scăderea vieții (ex: HP--)
            
            // Distrugem glonțul la impact
            Destroy(gameObject);
        }
    }
}