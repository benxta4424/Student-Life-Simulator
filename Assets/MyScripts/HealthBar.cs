using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    public int maxHealth = 100;
    private int currentHealth;
    public Slider slider; // Trage aici obiectul hpBoss

    void Start()
    {
        currentHealth = maxHealth;
        if (slider != null)
        {
            slider.maxValue = maxHealth;
            slider.value = maxHealth;
        }
    }

    // Aceasta este funcția care lipsea și dădea eroare!
    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        if (slider != null) 
        {
            slider.value = currentHealth;
        }

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        Debug.Log("Boss-ul a murit!");
        Destroy(gameObject); // Această linie face boss-ul să dispară definitiv
    }
}