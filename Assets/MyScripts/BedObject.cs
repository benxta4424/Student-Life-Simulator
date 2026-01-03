using UnityEngine;

public class BedObject : MonoBehaviour
{
    [SerializeField] private float energyRestoreAmount = 100f;
    [SerializeField] private int hoursToSleep = 8;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            GameManager gm = Object.FindAnyObjectByType<GameManager>();

            // Verificăm întâi dacă am găsit GameManager-ul
            if (gm != null)
            {
                // Verificăm dacă ești destul de obosit
                if (gm.energie > 80)
                {
                    Debug.Log("Nu ești destul de obosit ca să dormi!");
                    return;
                }

                // Dacă am trecut de verificare, dormim:
                gm.IncreaseEnergie(energyRestoreAmount);
                Debug.Log("Te-ai odihnit! Energia a fost refăcută.");

                TimeManager tm = Object.FindAnyObjectByType<TimeManager>();
                if (tm != null)
                {
                    tm.AddHours(hoursToSleep);
                }
            }
            else
            {
                Debug.LogError("BedObject: GameManager nu a fost găsit!");
            }
        }
    }
}