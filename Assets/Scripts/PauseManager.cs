using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseManager : MonoBehaviour
{
    [Header("Pause Panel")]
    public GameObject pausePanel; 
    public GameObject HealthCanvas;

    private bool isPaused = false;

    void Update()
    {
        // Toggle pause state when the Esc key is pressed
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isPaused)
            {
                ResumeGame();
            }
            else
            {
                PauseGame();
            }
        }
    }

    /// <summary>
    /// Pauses the game by setting time scale to 0 and showing the pause panel.
    /// </summary>
    public void PauseGame()
    {
        isPaused = true;
        Time.timeScale = 0f; // Stops the game
        pausePanel.SetActive(true); // Show the pause panel
        HealthCanvas.SetActive(false);
    }

    /// <summary>
    /// Resumes the game by setting time scale to 1 and hiding the pause panel.
    /// </summary>
    public void ResumeGame()
    {
        isPaused = false;
        Time.timeScale = 1f; // Resume the game
        pausePanel.SetActive(false); // Hide the pause panel
        HealthCanvas.SetActive(true);
        
    }

    /// <summary>
    /// Restarts the current scene.
    /// </summary>
    public void RestartGame()
    {
        Time.timeScale = 1f; // Ensure time scale is reset
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void LoadSceneByString(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }

    /// <summary>
    /// Exits the game.
    /// </summary>
    public void ExitGame()
    {
        Time.timeScale = 1f; // Ensure time scale is reset
        SceneManager.LoadScene("Main Menu");
    }
}

