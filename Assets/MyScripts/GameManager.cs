using TMPro.EditorUtilities;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [Header("Statistici Foame")]
    public float foame = 100f;
    [SerializeField] private float foameDecayAmount = 5f;//Cat scade la fiecare unitate de timp
    [SerializeField] private float foameDecayInterval = 5f;//La cat timp scade

    private UIManager uiManager;
    [Header("Cronometru Foame")]
    private float hungerTimer; // Cronometrul care numără până la 5 secunde
    private bool isInitialized = false; // Flag de verificare
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        // Awake se executa inaintea Start si asigura ca UIManager este gasit rapid
        uiManager = FindAnyObjectByType<UIManager>();
        if (uiManager != null)
        {
            // Apelam prima actualizare o data
            uiManager.UpdateStatsUI();
            isInitialized = true;
        }
        hungerTimer = foameDecayInterval; // Initializam cronometrul cu 5 secunde
    }
    void Start()
    {
        

    }
       void DecayFoame()
    {
        Debug.Log("Scădere Foame declanșată!"); // Linia de debug
        foame -= foameDecayAmount;
         foame = Mathf.Clamp(foame, 0f, 100f);
        if (uiManager != null)
        {
            uiManager.UpdateStatsUI();
        }
    }

    public void IncreaseFoame(float amount)
    {
        foame += amount;
        foame = Mathf.Clamp(foame, 0f, 100f);
        if (uiManager != null)
        {
            uiManager.UpdateStatsUI();
        }
    }
    // Update is called once per frame
    void Update()
    {
        if (!isInitialized) return; // Iese daca Managerul UI nu a fost gasit

        // 1. Scade timpul din cronometru
        hungerTimer -= Time.deltaTime;

        // 2. Verifica daca cronometrul a ajuns la zero
        if (hungerTimer <= 0)
        {
            // Resetam cronometrul
            hungerTimer = foameDecayInterval;

            // Apelam functia de scadere a foamei
            DecayFoame();
        }
    }
}
