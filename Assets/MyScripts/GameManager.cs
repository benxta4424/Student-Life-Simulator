using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    [Header("Statistici")]
    public float foame = 100f;
    public float energie = 100f;
    public float nivelInvatare = 0f;

    [SerializeField] private float foameDecayAmount = 5f;
    [SerializeField] private float foameDecayInterval = 5f;
    [SerializeField] private float energieDecayAmount = 3f;

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

    public void IncreaseFoame(float amount) 
    {
        foame = Mathf.Clamp(foame + amount, 0f, 100f);
        if (uiManager != null) uiManager.UpdateStatsUI();
    }

    public void IncreaseEnergie(float amount)
    {
        energie = Mathf.Clamp(energie + amount, 0f, 100f);
        if (uiManager != null) uiManager.UpdateStatsUI();
    }

    // NOU: Funcție pentru a consuma foame la Dash
    public bool ConsumeHungerForDash(float cost)
    {
        if (foame >= cost)
        {
            foame -= cost;
            if (uiManager != null) uiManager.UpdateStatsUI();
            return true; // Are destulă foame pentru dash
        }
        return false; // Nu are destulă foame
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

    void Update()
    {
        hungerTimer -= Time.deltaTime;
        if (hungerTimer <= 0)
        {
            hungerTimer = foameDecayInterval;
            // DecayFoame(); // MODIFICAT: Am comentat această linie ca să NU mai scadă singură
            DecayEnergie();
        }
    }

    void DecayFoame()
    {
        foame = Mathf.Clamp(foame - foameDecayAmount, 0f, 100f);
        if (uiManager == null) uiManager = FindAnyObjectByType<UIManager>();
        if (uiManager != null) uiManager.UpdateStatsUI();
    }

    void DecayEnergie()
    {
        energie = Mathf.Clamp(energie - energieDecayAmount, 0f, 100f);
        if (uiManager != null) uiManager.UpdateStatsUI();
    }

    void InitializeStats()
    {
        uiManager = Object.FindAnyObjectByType<UIManager>();
        if (uiManager != null) uiManager.UpdateStatsUI();
    }
}