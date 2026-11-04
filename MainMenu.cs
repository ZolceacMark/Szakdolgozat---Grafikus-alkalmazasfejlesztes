using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{ 

    public AudioSource menuMusic;
    [SerializeField] private Toggle tutorialToggle;

    public void Start()
    {
        if (PlayerPrefs.GetInt("ShowTutorial", 1) == 1)
        {
            tutorialToggle.isOn = true;
        }
        else
        {
            tutorialToggle.isOn = false;
        }
    }

    private void Awake()
    {
        if (!menuMusic.isPlaying)
        {
            menuMusic.Play();
        } 
    }

    public void ReflexGame25 ()
    {
        PlayerPrefs.SetInt("CourseLength", 25);
        if (tutorialToggle.isOn) {
            PlayerPrefs.SetInt("ShowTutorial", 1);
            PlayerPrefs.SetInt("OriginalShowTutorial", 1);
        }
        else
        {
            PlayerPrefs.SetInt("ShowTutorial", 0); 
            PlayerPrefs.SetInt("OriginalShowTutorial", 0);   
        }
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void ReflexGame50 ()
    {
        PlayerPrefs.SetInt("CourseLength", 50);
        if (tutorialToggle.isOn) {
            PlayerPrefs.SetInt("ShowTutorial", 1);
            PlayerPrefs.SetInt("OriginalShowTutorial", 1);
        }
        else
        {
            PlayerPrefs.SetInt("ShowTutorial", 0);  
            PlayerPrefs.SetInt("OriginalShowTutorial", 0);  
        }
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void ReflexGame75 ()
    {
        PlayerPrefs.SetInt("CourseLength", 75);
        if (tutorialToggle.isOn) {
            PlayerPrefs.SetInt("ShowTutorial", 1);
            PlayerPrefs.SetInt("OriginalShowTutorial", 1);
        }
        else
        {
            PlayerPrefs.SetInt("ShowTutorial", 0);
            PlayerPrefs.SetInt("OriginalShowTutorial", 0);    
        }
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void ReflexGame100 ()
    {
        PlayerPrefs.SetInt("CourseLength", 100);
        if (tutorialToggle.isOn) {
            PlayerPrefs.SetInt("ShowTutorial", 1);
            PlayerPrefs.SetInt("OriginalShowTutorial", 1);
        }
        else
        {
            PlayerPrefs.SetInt("ShowTutorial", 0);   
            PlayerPrefs.SetInt("OriginalShowTutorial", 0); 
        }
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

     public void MemoryGameEasy ()
    {
        if (tutorialToggle.isOn) {
            PlayerPrefs.SetInt("ShowTutorial", 1);
            PlayerPrefs.SetInt("OriginalShowTutorial", 1);
        }
        else
        {
            PlayerPrefs.SetInt("ShowTutorial", 0);   
            PlayerPrefs.SetInt("OriginalShowTutorial", 0); 
        }
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 2);
    }

    public void MemoryGameHard ()
    {
        if (tutorialToggle.isOn) {
            PlayerPrefs.SetInt("ShowTutorial", 1);
            PlayerPrefs.SetInt("OriginalShowTutorial", 1);
        }
        else
        {
            PlayerPrefs.SetInt("ShowTutorial", 0);   
            PlayerPrefs.SetInt("OriginalShowTutorial", 0); 
        }
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 3);
    }

     public void StroopGame ()
    {
        if (tutorialToggle.isOn) {
            PlayerPrefs.SetInt("ShowTutorial", 1);
            PlayerPrefs.SetInt("OriginalShowTutorial", 1);
        }
        else
        {
            PlayerPrefs.SetInt("ShowTutorial", 0);    
            PlayerPrefs.SetInt("OriginalShowTutorial", 0);
        }
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 3);
    }

    public void Quit ()
    {
        Debug.Log("Quit!");
        Application.Quit();
    }

}
