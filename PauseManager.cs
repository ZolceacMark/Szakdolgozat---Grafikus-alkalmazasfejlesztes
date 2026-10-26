using UnityEngine;
using UnityEngine.InputSystem;

public class PauseManager : MonoBehaviour
{
    [SerializeField] private GameObject pauseBackground;
    [SerializeField] private GameObject optionsPanel;



    void Update()
    {
        if (Keyboard.current.escapeKey.wasPressedThisFrame)
        {
            TogglePause();
        }
    }

    public void TogglePause()
    {
        pauseBackground.SetActive(true);
        optionsPanel.SetActive(true);
    }

    public void Resume()
    {
        pauseBackground.SetActive(false);
        optionsPanel.SetActive(false);
    }
}
