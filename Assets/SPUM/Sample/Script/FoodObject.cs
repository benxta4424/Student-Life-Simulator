using UnityEngine;

public class FoodObject : MonoBehaviour
{
 
    [SerializeField] private float hungerRestoreAmount = 30f;

    private GameManager gameManager;

    void Start()
    {

        gameManager = FindAnyObjectByType<GameManager>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        
        if (other.CompareTag("Player"))
        {
     
            if (gameManager != null)
            {
                gameManager.IncreaseFoame(hungerRestoreAmount);
            }
        }
    }
}