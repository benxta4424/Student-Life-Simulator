using TMPro.EditorUtilities;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [Header("Statistici Foame")]
    public float foame = 100f;
    [SerializeField] private float foameDecayAmount = 5f;//Cat scade la fiecare unitate de timp
    [SerializeField] private float foameDecayInterval = 5f;//La cat timp scade
    private UIManager uiManager;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        uiManager =FindAnyObjectByType<UIManager>();
        InvokeRepeating("DecayFoame", foameDecayInterval, foameDecayInterval);

    }
    void DecayFoame()
    {
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
        
    }
}
