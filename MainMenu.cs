using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;

public class MainMenu : MonoBehaviour
{ 

    public AudioSource menuMusic;

    private void Awake()
    {
        if (!menuMusic.isPlaying)
        {
            menuMusic.Play();
        } 
    }

    public void ReflexGame ()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

     public void MemoryGame ()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 2);
    }

     public void StroopGame ()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 3);
    }

    public void Quit ()
    {
        Debug.Log("Quit!");
        Application.Quit();
    }

}
