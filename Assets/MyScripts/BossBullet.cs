using UnityEngine;

public class BossBullet : MonoBehaviour
{
    public float speed = 8f;
    private Rigidbody2D rb;
    private Vector2 moveDirection;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.gravityScale = 0; // Glontul nu trebuie sa cada

        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player != null)
        {
            // Calculeaza directia spre jucator in momentul tragerii
            moveDirection = (player.transform.position - transform.position).normalized;
        }
        else
        {
            moveDirection = Vector2.down; // Directie de siguranta daca nu gaseste jucatorul
        }

        // Distruge glontul dupa 4 secunde ca sa nu incarce memoria
        Destroy(gameObject, 4f);
    }

    void Update()
    {
        // Misca glontul in directia calculata la start
        transform.Translate(moveDirection * speed * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Daca loveste jucatorul
        if (collision.CompareTag("Player"))
        {
            Debug.Log("L-ai lovit pe jucator!");
            // Aici poti pune logica de scadere viata jucator
            Destroy(gameObject); // Distruge DOAR glontul, nu si Boss-ul!
        }
    }
}