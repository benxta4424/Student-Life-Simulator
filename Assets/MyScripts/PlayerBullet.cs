using UnityEngine;

public class PlayerBullet : MonoBehaviour
{
    // Valorile sunt 'private const' pentru a nu putea fi modificate din Inspector
    private const float BAZA_DAMAGE_INFIM = 0.01f; 
    private const float MULTIPLICATOR_STUDIU = 0.5f; 
    
    public float speed = 45f;

    void Start()
    {
        if (GetComponent<Rigidbody2D>() != null)
        {
            GetComponent<Rigidbody2D>().linearVelocity = transform.right * speed;
        }
        Destroy(gameObject, 2f);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        // ATENȚIE: Căutăm 'BossHealth', nu 'HealthBar', pentru a se potrivi cu scriptul boss-ului
        BossHealth boss = other.GetComponent<BossHealth>(); 

        if (boss != null)
        {
            float nivel = 0;
            if (GameManager.instance != null)
            {
                nivel = GameManager.instance.nivelInvatare;
            }

            float damageCalculat = BAZA_DAMAGE_INFIM + (nivel * MULTIPLICATOR_STUDIU);

            // Mathf.RoundToInt rotunjește 0.5 la cel mai apropiat număr par (0)
            boss.TakeDamage(Mathf.RoundToInt(damageCalculat)); 
            
            Debug.Log("Impact! Nivel Studiu: " + nivel + " | Damage aplicat: " + Mathf.RoundToInt(damageCalculat));
            Destroy(gameObject);
        }
    }
}