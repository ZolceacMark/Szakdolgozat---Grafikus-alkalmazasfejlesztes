using UnityEngine;
using UnityEngine.Video;

public class ReflexTutorialVideo : MonoBehaviour
{
    [SerializeField] private VideoPlayer reflexTutorial;
    [SerializeField] private AudioSource reflexTutorialAudioSource;
    [SerializeField] private GameObject replayButton;
    [SerializeField] private GameObject pausePanel;

    private bool isPaused;

    private void Update()
    {
        if (replayButton.activeSelf)
            return;

        if (pausePanel.activeSelf && !isPaused)
        {
            reflexTutorial.Pause();
            isPaused = true;
        }
        else if (!pausePanel.activeSelf && isPaused)
        {
            reflexTutorial.Play();
            isPaused = false;
        }
    }

    private void Start()
    {
        reflexTutorial.audioOutputMode = VideoAudioOutputMode.AudioSource;
        reflexTutorial.EnableAudioTrack(0, true);
        reflexTutorial.SetTargetAudioSource(0, reflexTutorialAudioSource);  

        reflexTutorial.playOnAwake = false;
        reflexTutorial.isLooping = false;
        reflexTutorial.loopPointReached += ShowReplayButton; 

        replayButton.SetActive(false);
    }

    private void ShowReplayButton(VideoPlayer video)
    {
        replayButton.SetActive(true);    
    }

    public void PlayVideo()
    {
        replayButton.SetActive(false);
        reflexTutorial.Play();
    }

    public void ReplayTutorial()
    {
        replayButton.SetActive(false);

        reflexTutorial.Stop();
        reflexTutorial.Play();
    }

    private void OnDisable()
    {   
        if (reflexTutorial != null)
        {
            reflexTutorial.Stop();
            reflexTutorial.frame = 0;
        }
    }

    private void OnDestroy()
    {   
        if (reflexTutorial != null)
            reflexTutorial.loopPointReached -= ShowReplayButton;
    }
}
