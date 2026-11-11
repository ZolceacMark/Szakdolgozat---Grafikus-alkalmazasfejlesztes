using UnityEngine;
using UnityEngine.Video;

public class MemoryEasyTutorial : MonoBehaviour
{
    [SerializeField] private VideoPlayer memoryEasyTutorial;
    [SerializeField] private AudioSource memoryEasyTutorialAudioSource;
    [SerializeField] private GameObject replayButton;
    [SerializeField] private GameObject pausePanel;

    private bool isPaused;

    private void Update()
    {
        if (replayButton.activeSelf)
            return;

        if (pausePanel.activeSelf && !isPaused)
        {
            memoryEasyTutorial.Pause();
            isPaused = true;
        }
        else if (!pausePanel.activeSelf && isPaused)
        {
            memoryEasyTutorial.Play();
            isPaused = false;
        }
    }

    private void Start()
    {
        memoryEasyTutorial.playOnAwake = false;
        memoryEasyTutorial.isLooping = false;
        memoryEasyTutorial.loopPointReached += ShowReplayButton; 

        replayButton.SetActive(false);
    }

    private void ShowReplayButton(VideoPlayer video)
    {
        replayButton.SetActive(true);    
    }

    public void PlayVideo()
    {
        replayButton.SetActive(false);

        Debug.Log("Audio tracks: " + memoryEasyTutorial.audioTrackCount);
        Debug.Log("Audio enabled: " + memoryEasyTutorial.IsAudioTrackEnabled(0));
        Debug.Log("Audio source: " + memoryEasyTutorial.GetTargetAudioSource(0));
        Debug.Log("Audio output mode: " + memoryEasyTutorial.audioOutputMode);

        memoryEasyTutorial.Play();
    }

    public void ReplayTutorial()
    {
        replayButton.SetActive(false);

        memoryEasyTutorial.Stop();
        memoryEasyTutorial.Play();
    }

    private void OnEnable()
    {
        memoryEasyTutorial.audioOutputMode = VideoAudioOutputMode.AudioSource;
        memoryEasyTutorial.EnableAudioTrack(0, true);
        memoryEasyTutorial.SetTargetAudioSource(0, memoryEasyTutorialAudioSource);
        PlayVideo();
    }

    private void OnDisable()
    {
        if (memoryEasyTutorial != null)
        {
            memoryEasyTutorial.Stop();
            memoryEasyTutorial.frame = 0;
        }
    }

    private void OnDestroy()
    {   if (memoryEasyTutorial != null)
            memoryEasyTutorial.loopPointReached -= ShowReplayButton;
    }
}
