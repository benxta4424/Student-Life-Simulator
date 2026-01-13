using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerStats : MonoBehaviour
{
    [Header("Foame (Hunger)")]
    public float foame = 100f;
    public float scadereFoame = 2f; // Cat scade la fiecare interval
    public float intervalFoame = 5f; // Secunde
    public Slider foameSlider; // Trage aici foameSlider din UIManager
    public TextMeshProUGUI foameText; // Trage aici foameText din UIManager

    [Header("Energie")]
    public float energie = 100f;
    public Slider energieSlider; 
    public TextMeshProUGUI energieText; // Trage aici energieText

    private float timerFoame;

    void Start()
    {
        timerFoame = intervalFoame;
        if (foameSlider != null) foameSlider.maxValue = 100;
        if (energieSlider != null) energieSlider.maxValue = 100;
    }

    void Update()
    {
        // Logica de scadere foame
        timerFoame -= Time.deltaTime;
        if (timerFoame <= 0)
        {
            foame -= scadereFoame;
            if (foame < 0) foame = 0;
            timerFoame = intervalFoame;
        }

        // Actualizare UI - Smooth
        if (foameSlider != null) 
            foameSlider.value = Mathf.Lerp(foameSlider.value, foame, Time.deltaTime * 2f);
        
        if (foameText != null) 
            foameText.text = "Foame: " + Mathf.Round(foame) + "%";

        if (energieText != null)
            energieText.text = "Energie: " + Mathf.Round(energie) + "%";
    }
}