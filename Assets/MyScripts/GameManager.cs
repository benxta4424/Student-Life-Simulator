using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [Header("Statistici Foame")]
    public float foame = 100f;
    [SerializeField] private float foameDecayAmount = 5f;
    [SerializeField] private float foameDecayInterval = 5f;

    [Header("Statistici Energie")]
    public float energie = 100f;
    [SerializeField] private float energieDecayAmount = 3f;
    [SerializeField] private float energyRestoreAmount = 100f;
    [SerializeField] private int hoursToSleep = 8;

    private UIManager uiManager;
    private float hungerTimer;
    private bool isInitialized = false;

    [Header("Statistici Progresie")]
    public float nivelInvatare = 0f; // Nivelul tău actual

    // Funcția care verifică dacă ai destulă energie și crește învățarea
    public void Study(float costEnergie, float gainInvatare)
    {
        if (energie >= costEnergie)
        {
            energie -= costEnergie; // Consumă energia
            nivelInvatare += gainInvatare; // Crește nivelul de învățare
            
            // Limităm valorile între 0 și 100 pentru siguranță
            energie = Mathf.Clamp(energie, 0f, 100f); 
            
            if (uiManager != null) uiManager.UpdateStatsUI(); // Actualizează barele și textele
            Debug.Log("Ai învățat! Nivel: " + nivelInvatare + " | Energie rămasă: " + energie);
        }
        else
        {
            Debug.Log("Ești prea obosit pentru a studia!");
        }
    }

    void Awake()
    {
        // Folosim varianta modernă pentru a evita warning-ul din imaginea ta
        if (GameObject.FindObjectsByType<GameManager>(FindObjectsSortMode.None).Length > 1)
        {
            Destroy(gameObject);
            return;
        }

        // IMPORTANT: Spunem Unity să păstreze tot obiectul "Systems_Stats"
        // (gameObject se referă la obiectul pe care stă scriptul)
        DontDestroyOnLoad(gameObject);

        InitializeStats();
    }

    // Funcție separată pentru a căuta UI-ul la început și la schimbarea scenei
    void InitializeStats()
    {
        uiManager = FindAnyObjectByType<UIManager>();
        if (uiManager != null)
        {
            uiManager.UpdateStatsUI();
            isInitialized = true; // Permitem cronometrului să ruleze
        }
    }

    void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    // Această funcție "repară" legătura cu UI-ul în scena nouă
    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        InitializeStats();
    }

    void Update()
    {
        if (!isInitialized) return;

        hungerTimer -= Time.deltaTime;
        if (hungerTimer <= 0)
        {
            hungerTimer = foameDecayInterval;
            DecayFoame();
            DecayEnergie();
        }
    }

    // --- Restul funcțiilor tale (DecayFoame, IncreaseEnergie etc.) rămân la fel ---
    void DecayFoame()
    {
        foame = Mathf.Clamp(foame - foameDecayAmount, 0f, 100f);
        if (uiManager != null) uiManager.UpdateStatsUI();
    }
    public void IncreaseFoame(float amount) 
    {
        foame = Mathf.Clamp(foame + amount, 0f, 100f);
        if (uiManager != null)
        {
            uiManager.UpdateStatsUI();
        }
    }
    void DecayEnergie()
    {
        energie = Mathf.Clamp(energie - energieDecayAmount, 0f, 100f);
        if (uiManager != null) uiManager.UpdateStatsUI();
    }

    public void IncreaseEnergie(float amount)
    {
        energie = Mathf.Clamp(energie + amount, 0f, 100f);
        if (uiManager != null) uiManager.UpdateStatsUI();
    }
}