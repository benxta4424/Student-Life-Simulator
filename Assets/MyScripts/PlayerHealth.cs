using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    public int maxHealth = 100;
    private float currentHealth;
    public Slider healthSlider; 
    public float smoothSpeed = 5f;

    private Animator anim;
    private bool isDead = false;

    void Start()
    {
        currentHealth = maxHealth;
        anim = GetComponent<Animator>();
        if (healthSlider != null) { healthSlider.maxValue = maxHealth; healthSlider.value = maxHealth; }
    }

    void Update()
    {
        if (healthSlider != null && healthSlider.value != currentHealth)
            healthSlider.value = Mathf.Lerp(healthSlider.value, currentHealth, Time.deltaTime * smoothSpeed);
    }

    public void TakeDamage(int damage)
    {
        if (isDead) return;
        currentHealth -= damage;
        if (currentHealth <= 0) { currentHealth = 0; Die(); }
    }

    void Die()
    {
        if (isDead) return;
        isDead = true;

        if (anim != null) anim.SetTrigger("DeathTrigger");
        
        // Dezactivează mișcarea
        MonoBehaviour movement = GetComponent("UndertaleMovement") as MonoBehaviour;
        if (movement != null) movement.enabled = false;

        // Trimite moartea către GameManager
        if (GameManager.instance != null)
        {
            GameManager.instance.HandleDeath();
        }
    }
}