using UnityEngine;
using UnityEngine.UI;

public class BossHealth : MonoBehaviour
{
    public int maxHealth = 5000; 
    protected int currentHealth; // ptr ca am doi bosi si folosesc ptr amandoi vreau sa am separat
    public Slider hpSlider;


    protected virtual void Start()
    {
        currentHealth = maxHealth;
        if (hpSlider != null)
        {
            hpSlider.maxValue = maxHealth;
            hpSlider.value = maxHealth;
        }
    }

    // virtual permite noului boss să își scrie propria logică de damage
    public virtual void TakeDamage(int damage)
    {
        currentHealth -= damage;
        if (hpSlider != null) hpSlider.value = currentHealth;
        if (currentHealth <= 0) Die();
    }

    protected void Die()
    {
        Debug.Log(gameObject.name + " a fost învins!");
        Destroy(gameObject);
    }
}