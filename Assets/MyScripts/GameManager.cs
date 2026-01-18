using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections.Generic;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    [Header("Statistici")]
    public float foame = 100f;
    public float energie = 100f;
    public float nivelInvatare = 0f;

    [SerializeField] private float foameDecayInterval = 5f;
    [SerializeField] private float energieDecayAmount = 3f;

    // Dicționar pentru șanse paralele: cheia este numele scenei
    private Dictionary<string, int> bossAttempts = new Dictionary<string, int>();

    private UIManager uiManager;
    private float hungerTimer;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
            return;
        }
        InitializeStats();
    }

    // NOU: Gestionează morțile diferit pentru fiecare boss
    public void HandleDeath()
    {
        string currentScene = SceneManager.GetActiveScene().name;
        
        if (!bossAttempts.ContainsKey(currentScene)) 
            bossAttempts[currentScene] = 0;
        
        bossAttempts[currentScene]++;
        int deaths = bossAttempts[currentScene];

        string message = "";
        if (deaths == 1) message = "Vino in P2";
        else if (deaths == 2) message = "Vino in P3";
        else message = "PICAT";

        if (uiManager == null) uiManager = Object.FindAnyObjectByType<UIManager>();
        if (uiManager != null) uiManager.ShowFailMessage(message);

        // Dacă a murit de 3 ori, resetăm contorul pentru acest boss
        if (deaths >= 3) bossAttempts[currentScene] = 0;
    }

    public void Study(float costEnergie, float gainInvatare)
    {
        if (energie >= costEnergie)
        {
            energie -= costEnergie;
            nivelInvatare += gainInvatare;
            if (uiManager != null) uiManager.UpdateStatsUI();
        }
    }

    public bool ConsumeHungerForDash(float cost)
    {
        if (foame >= cost)
        {
            foame -= cost;
            if (uiManager != null) uiManager.UpdateStatsUI();
            return true;
        }
        return false;
    }

    public void IncreaseFoame(float amount) { foame = Mathf.Clamp(foame + amount, 0f, 100f); if (uiManager != null) uiManager.UpdateStatsUI(); }
    public void IncreaseEnergie(float amount) { energie = Mathf.Clamp(energie + amount, 0f, 100f); if (uiManager != null) uiManager.UpdateStatsUI(); }

    void Update()
    {
        hungerTimer -= Time.deltaTime;
        if (hungerTimer <= 0)
        {
            hungerTimer = foameDecayInterval;
            DecayEnergie();
        }
    }

    void DecayEnergie()
    {
        energie = Mathf.Clamp(energie - energieDecayAmount, 0f, 100f);
        if (uiManager != null) uiManager.UpdateStatsUI();
    }

    public void InitializeStats()
    {
        uiManager = Object.FindAnyObjectByType<UIManager>();
        if (uiManager != null) uiManager.UpdateStatsUI();
    }
}