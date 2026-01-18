using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    [Header("Bari de Statistici")]
    public Slider foameSlider;
    public TextMeshProUGUI foameText;
    public TextMeshProUGUI energieText; 
    public TextMeshProUGUI invatatText; 

    [Header("Ceas")]
    public TextMeshProUGUI timeText;

    [Header("Mesaje Moarte")]
    public GameObject failPanel; // Trebuie creat în Unity (Panel)
    public TextMeshProUGUI failMessageText; // Textul din interiorul Panelului

    void Awake()
    {
        if (foameSlider != null) foameSlider.maxValue = 100f;
        if (failPanel != null) failPanel.SetActive(false);
        UpdateStatsUI();
    }

    public void UpdateStatsUI() 
    {
        if (GameManager.instance == null) return;

        if (foameSlider != null) foameSlider.value = GameManager.instance.foame;
        if (foameText != null) foameText.text = "Foame: " + Mathf.Floor(GameManager.instance.foame);
        if (energieText != null) energieText.text = "Energie: " + Mathf.Floor(GameManager.instance.energie) + "%";
        if (invatatText != null) invatatText.text = "Nivel: " + Mathf.Floor(GameManager.instance.nivelInvatare);
    }

    public void ShowFailMessage(string msg)
    {
        if (failPanel != null) failPanel.SetActive(true);
        if (failMessageText != null) failMessageText.text = msg;
        
        // Așteaptă 3 secunde și apoi resetează scena
        Invoke("HideFailPanel", 3f);
    }

    void HideFailPanel()
    {
        if (failPanel != null) failPanel.SetActive(false);
        SceneManager.LoadScene("Cantina"); // Sau scena de start
    }

    public void UpdateTimeUI(int hours, int minutes)
    {
        if (timeText != null)
            timeText.text = string.Format("{0:00}:{1:00}", hours, minutes);
    }
}