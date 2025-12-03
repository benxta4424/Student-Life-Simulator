using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIManager : MonoBehaviour
{
    private GameManager gameManager;

    [Header("Bari de Statistici")]
    public Slider foameSlider;

    [Header("Text Informații")]
    public TextMeshProUGUI foameText;


    void Awake()
    {
        gameManager = FindAnyObjectByType<GameManager>();

        if (gameManager == null)
        {
             Debug.LogError("GameManager nu a fost găsit în scenă! UI-ul nu va funcționa corect.");
        }

        
        if (foameSlider != null)
        {
            foameSlider.maxValue = 100f;
        }

    
        UpdateStatsUI();
    }

   
    public void UpdateStatsUI() 
    {
        if (gameManager == null) return;

        
        if (foameSlider != null)
        {
            foameSlider.value = gameManager.foame;
        }
        if (foameText != null)
        {
            foameText.text = "Foame: " + Mathf.Floor(gameManager.foame);
        }
    }
}