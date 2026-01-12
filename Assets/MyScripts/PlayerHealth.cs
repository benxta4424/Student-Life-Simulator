using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerHealth : MonoBehaviour
{
    public int maxHealth = 100;
    private float currentHealth; // Folosim float pentru precizie la animație
    
    public Slider healthSlider; 
    public float smoothSpeed = 5f; // Viteza cu care "curge" bara de viață

    private Animator anim;
    private bool isDead = false;

    void Start()
    {
        currentHealth = maxHealth;
        anim = GetComponent<Animator>();

        if (healthSlider != null)
        {
            healthSlider.maxValue = maxHealth;
            healthSlider.value = maxHealth;
        }
    }

    void Update()
    {
        // Această linie face magia: slider-ul se mișcă lin către valoarea vieții actuale
        if (healthSlider != null && healthSlider.value != currentHealth)
        {
            healthSlider.value = Mathf.Lerp(healthSlider.value, currentHealth, Time.deltaTime * smoothSpeed);
        }
    }

    public void TakeDamage(int damage)
    {
        if (isDead) return;

        currentHealth -= damage;

        if (currentHealth <= 0)
        {
            currentHealth = 0;
            Die();
        }
    }

    void Die()
    {
        if (isDead) return;
        isDead = true;

        if (anim != null) anim.SetTrigger("DeathTrigger");
        
        // Dezactivează mișcarea
        MonoBehaviour movement = GetComponent("UndertaleMovement") as MonoBehaviour;
        if (movement != null) movement.enabled = false;

        Invoke("LoadCantinaScene", 3f);
    }

    void LoadCantinaScene()
    {
        SceneManager.LoadScene("Cantina");
    }
}