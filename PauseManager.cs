using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PauseManager : MonoBehaviour
{
    [SerializeField] private GameObject pauseBackground;
    [SerializeField] private GameObject optionsPanel;
    [SerializeField] private GameObject gamePanel;
    [SerializeField] private GameObject backButton;
    [SerializeField] private GameObject inGameBackButton;
    [SerializeField] private GameObject inGameRestartButton;
    [SerializeField] private GameObject exitButton;



    void Update()
    {
        if (Keyboard.current.escapeKey.wasPressedThisFrame && optionsPanel.activeSelf) 
        {
            Resume();
        }
        else if (Keyboard.current.escapeKey.wasPressedThisFrame && !gamePanel.activeSelf)
        {
            TogglePauseTutorialPanel();   
        }
        else if (Keyboard.current.escapeKey.wasPressedThisFrame && gamePanel.activeSelf) 
        {
            TogglePauseGamePanel();
        }
    }

    public void TogglePauseTutorialPanel()
    {
        pauseBackground.SetActive(true);
        optionsPanel.SetActive(true);
        backButton.SetActive(true);
        inGameBackButton.SetActive(false);
        exitButton.SetActive(false);
    }

    public void TogglePauseGamePanel()
    {
        Time.timeScale = 0f;
        pauseBackground.SetActive(true);
        optionsPanel.SetActive(true);
        backButton.SetActive(false);
        inGameBackButton.SetActive(true);
        inGameRestartButton.SetActive(true);
        exitButton.SetActive(true);
    }

    public void Resume()
    {
        Time.timeScale = 1f;
        pauseBackground.SetActive(false);
        optionsPanel.SetActive(false);
    }

    public void ExitGame()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
    }
}
