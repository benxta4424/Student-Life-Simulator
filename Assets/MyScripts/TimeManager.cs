using UnityEngine;

public class TimeManager : MonoBehaviour
{
    [Header("Setãri Timp")]
    public float timeMultiplier = 60f; // 1 secundã realã = 1 minut în joc
    public int startHour = 8;

    private float elapsedSeconds;
    private int currentMinutes;
    private int currentHours;

    private UIManager uiManager;

    void Start()
    {
        uiManager = FindAnyObjectByType<UIManager>();
        currentHours = startHour;
    }

    void Update()
    {
        // Calculãm trecerea timpului
        elapsedSeconds += Time.deltaTime * timeMultiplier;

        if (elapsedSeconds >= 60f)
        {
            elapsedSeconds = 0;
            currentMinutes++;

            if (currentMinutes >= 60)
            {
                currentMinutes = 0;
                currentHours++;

                if (currentHours >= 24)
                {
                    currentHours = 0;
                }
            }

            // Actualizãm UI-ul doar când se schimbã minutul
            if (uiManager != null)
            {
                uiManager.UpdateTimeUI(currentHours, currentMinutes);
            }
        }
    }
}
