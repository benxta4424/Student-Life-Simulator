using UnityEngine;
using UnityEngine.UI;

public class BossHealth : MonoBehaviour
{
    public int maxHealth = 100;
    private int currentHealth;
    public Slider hpSlider; // Trage aici hpBoss din Canvas

    void Start()
    {
        currentHealth = maxHealth;
        if (hpSlider != null)
        {
            hpSlider.maxValue = maxHealth;
            hpSlider.value = maxHealth;
        }
    }

    // Funcția care scade viața
    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        
        if (hpSlider != null)
        {
            hpSlider.value = currentHealth;
        }

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        Debug.Log("Boss-ul a murit!");
        Destroy(gameObject); // Această linie șterge boss-ul din joc
    }
}