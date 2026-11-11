using UnityEngine;
using UnityEngine.Video;

public class MemoryHardTutorial : MonoBehaviour
{
    [SerializeField] private VideoPlayer memoryHardTutorial;
    [SerializeField] private AudioSource memoryHardTutorialAudioSource;
    [SerializeField] private GameObject replayButton;
    [SerializeField] private GameObject pausePanel;

    private bool isPaused;

    private void Update()
    {
        if (replayButton.activeSelf)
            return;

        if (pausePanel.activeSelf && !isPaused)
        {
            memoryHardTutorial.Pause();
            isPaused = true;
        }
        else if (!pausePanel.activeSelf && isPaused)
        {
            memoryHardTutorial.Play();
            isPaused = false;
        }
    }

    private void Start()
    {
        memoryHardTutorial.audioOutputMode = VideoAudioOutputMode.AudioSource;
        memoryHardTutorial.EnableAudioTrack(0, true);
        memoryHardTutorial.SetTargetAudioSource(0, memoryHardTutorialAudioSource);  

        memoryHardTutorial.playOnAwake = false;
        memoryHardTutorial.isLooping = false;
        memoryHardTutorial.loopPointReached += ShowReplayButton; 

        replayButton.SetActive(false);
    }

    private void ShowReplayButton(VideoPlayer video)
    {
        replayButton.SetActive(true);    
    }

    public void PlayVideo()
    {
        replayButton.SetActive(false);
        memoryHardTutorial.Play();
    }

    public void ReplayTutorial()
    {
        replayButton.SetActive(false);

        memoryHardTutorial.Stop();
        memoryHardTutorial.Play();
    }

    private void OnDisable()
    {   
        if (memoryHardTutorial != null)
        {
            memoryHardTutorial.Stop();
            memoryHardTutorial.frame = 0;
        }
    }

    private void OnDestroy()
    {   
        if (memoryHardTutorial != null)
            memoryHardTutorial.loopPointReached -= ShowReplayButton;
    }
}
