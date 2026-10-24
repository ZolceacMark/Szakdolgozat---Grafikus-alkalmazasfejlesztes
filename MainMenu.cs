using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{ 

    public AudioSource menuMusic;
    [SerializeField] private Toggle tutorialToggle;

    private void Awake()
    {
        if (!menuMusic.isPlaying)
        {
            menuMusic.Play();
        } 
    }

    public void ReflexGame ()
    {
        if (tutorialToggle.isOn) {
            PlayerPrefs.SetInt("ShowTutorial", 1);
        }
        else
        {
            PlayerPrefs.SetInt("ShowTutorial", 0);    
        }
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

     public void MemoryGame ()
    {
        if (tutorialToggle.isOn) {
            PlayerPrefs.SetInt("ShowTutorial", 1);
        }
        else
        {
            PlayerPrefs.SetInt("ShowTutorial", 0);    
        }
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 2);
    }

     public void StroopGame ()
    {
        if (tutorialToggle.isOn) {
            PlayerPrefs.SetInt("ShowTutorial", 1);
        }
        else
        {
            PlayerPrefs.SetInt("ShowTutorial", 0);    
        }
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 3);
    }

    public void Quit ()
    {
        Debug.Log("Quit!");
        Application.Quit();
    }

}
