using UnityEngine;
using UnityEngine.SceneManagement;

public class DoorTransition : MonoBehaviour
{
    public GameObject transitionMenuPanel;
    
    // SCHIMBAT: Variabila se așteaptă acum la tipul UndertaleMovement
    public UndertaleMovement playerMovementScript; 
    // Notă: Va trebui să atribui manual scriptul UndertaleMovement de pe jucător în acest slot,
    // în Inspectorul Unity, pe obiectul Ușă.

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("Player"))
        { 
            // Verificăm NULL înainte de a folosi
            if (playerMovementScript != null)
            {
                playerMovementScript.enabled = false;
            }
            
            Time.timeScale = 0f; // Pause the game
            
            // Verificăm NULL înainte de a folosi
            if (transitionMenuPanel != null)
            {
                transitionMenuPanel.SetActive(true);
            }
        }
    }

    public void LoadNewScene(string sceneName)
    {
        Time.timeScale = 1f; // Resume the game
        SceneManager.LoadScene(sceneName);
    }

    public void CloseMenu()
    {
        if (transitionMenuPanel != null)
        {
            transitionMenuPanel.SetActive(false);
        }
        
        // Verificăm NULL înainte de a folosi
        if (playerMovementScript != null)
        {
            playerMovementScript.enabled = true;
        }
        
        Time.timeScale = 1f; // Resume the game
    }
}