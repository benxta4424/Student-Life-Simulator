using UnityEngine;

public class PlayerBullet : MonoBehaviour
{
    public float speed = 100f; //
    public int damage = 10;

    void Start()
    {
        GetComponent<Rigidbody2D>().linearVelocity = transform.right * speed;
        Destroy(gameObject, 2f);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        // Căutăm scriptul numit HealthBar pe obiectul lovit
        HealthBar health = other.GetComponent<HealthBar>();

        if (health != null)
        {
            health.TakeDamage(damage); // Acum va funcționa fără eroare!
            Destroy(gameObject);
        }
    }
}