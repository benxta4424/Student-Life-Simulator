using UnityEngine;

public class BossAI : MonoBehaviour
{
    public float moveSpeed = 3f;
    private Transform player;
    public GameObject bulletPrefab;
    public Transform firePoint;
    public float fireRate = 1.5f;
    private float nextFireTime;

    void Start()
    {
        // Gasim jucatorul dupa tag
        GameObject p = GameObject.FindGameObjectWithTag("Player");
        if (p != null) player = p.transform;
        
        // Ne asiguram ca boss-ul nu cade de pe harta
        if(GetComponent<Rigidbody2D>()) 
            GetComponent<Rigidbody2D>().gravityScale = 0;
    }

    void Update()
    {
        if (player == null) return;

        // Miscare spre jucator
        transform.position = Vector2.MoveTowards(transform.position, player.position, moveSpeed * Time.deltaTime);

        // tras
        if (Time.time >= nextFireTime)
        {
            Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
            nextFireTime = Time.time + fireRate;
        }
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("Boss-ul a atins: " + other.name);

    }
}