using UnityEngine;
using UnityEngine.Video;

public class StroopTutorial : MonoBehaviour
{
    [SerializeField] private VideoPlayer stroopTutorial;
    [SerializeField] private AudioSource stroopTutorialAudioSource;
    [SerializeField] private GameObject replayButton;
    [SerializeField] private GameObject pausePanel;

    private bool isPaused;

    private void Update()
    {
        if (replayButton.activeSelf)
            return;

        if (pausePanel.activeSelf && !isPaused)
        {
            stroopTutorial.Pause();
            isPaused = true;
        }
        else if (!pausePanel.activeSelf && isPaused)
        {
            stroopTutorial.Play();
            isPaused = false;
        }
    }

    private void Start()
    {
        stroopTutorial.audioOutputMode = VideoAudioOutputMode.AudioSource;
        stroopTutorial.EnableAudioTrack(0, true);
        stroopTutorial.SetTargetAudioSource(0, stroopTutorialAudioSource);  

        stroopTutorial.playOnAwake = false;
        stroopTutorial.isLooping = false;
        stroopTutorial.loopPointReached += ShowReplayButton; 

        replayButton.SetActive(false);
    }

    private void ShowReplayButton(VideoPlayer video)
    {
        replayButton.SetActive(true);    
    }

    public void PlayVideo()
    {
        replayButton.SetActive(false);
        stroopTutorial.Play();
    }

    public void ReplayTutorial()
    {
        replayButton.SetActive(false);

        stroopTutorial.Stop();
        stroopTutorial.Play();
    }

    private void OnDisable()
    {   
        if (stroopTutorial != null)
        {
            stroopTutorial.Stop();
            stroopTutorial.frame = 0;
        }
    }

    private void OnDestroy()
    {   
        if (stroopTutorial != null)
            stroopTutorial.loopPointReached -= ShowReplayButton;
    }
}
