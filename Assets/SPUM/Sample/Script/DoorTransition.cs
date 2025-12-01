using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DoorTransition : MonoBehaviour
{
    public GameObject transitionMenuPanel;
    public TopDownMovement playerMovementScript;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("Player"))
        { 
                playerMovementScript.enabled = false;
              Time.timeScale = 0f; // Pause the game
                transitionMenuPanel.SetActive(true);

        }
    }

    public void LoadNewScene(string sceneName)
    {
        Time.timeScale = 1f; // Resume the game
        SceneManager.LoadScene(sceneName);
    }

    public void CloseMenu()
        {
        transitionMenuPanel.SetActive(false);
        playerMovementScript.enabled = true;
        Time.timeScale = 1f; // Resume the game

    }
    
}
