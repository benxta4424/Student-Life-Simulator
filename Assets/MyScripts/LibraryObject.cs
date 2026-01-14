using UnityEngine;

public class LibraryObject : MonoBehaviour
{
    [Header("Setări Studiu")]
    [SerializeField] private float costEnergie = 15f;
    [SerializeField] private float puncteInvatare = 5f;

    private void OnTriggerEnter2D(Collider2D other)
    {
        // Verificăm dacă cel care atinge raftul este Jucătorul
        if (other.CompareTag("Player"))
        {
            GameManager gm = Object.FindAnyObjectByType<GameManager>();
            
            if (gm != null)
            {
                gm.Study(costEnergie, puncteInvatare);
            }
        }
    }
}