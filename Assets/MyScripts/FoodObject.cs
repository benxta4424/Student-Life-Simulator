using UnityEngine;

public class FoodObject : MonoBehaviour
{
 
    [SerializeField] public float hungerRestoreAmount = 30f;

    public GameManager gameManager;

    void Start()
    {

        gameManager = FindAnyObjectByType<GameManager>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("Trigger atins de: " + other.gameObject.name);
        if (other.CompareTag("Player"))
        {
     
            if (gameManager != null)
            {
                gameManager.IncreaseFoame(hungerRestoreAmount);
            }
        }
    }
}