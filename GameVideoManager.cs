using UnityEngine;
using UnityEngine.Video;
public class GameVideoManager : MonoBehaviour
{
    [SerializeField] private VideoPlayer tvOnVideoPlayer;
    [SerializeField] private VideoPlayer staticVideoPlayer;
    [SerializeField] private GameObject tvOnRawImage;
    [SerializeField] private GameObject staticRawImage;
    [SerializeField] private GameObject blackScreen;

    [SerializeField] private AudioSource videoAudioSource;

    private void Start()
    {
        tvOnVideoPlayer.audioOutputMode = VideoAudioOutputMode.AudioSource;
        tvOnVideoPlayer.EnableAudioTrack(0, true);
        tvOnVideoPlayer.SetTargetAudioSource(0, videoAudioSource);

        tvOnVideoPlayer.isLooping = false;
        tvOnVideoPlayer.playOnAwake = false;
        tvOnVideoPlayer.sendFrameReadyEvents = true;

        staticVideoPlayer.playOnAwake = false;
        staticVideoPlayer.isLooping = true;

        tvOnVideoPlayer.frameReady += VideoLoadCheck;
        tvOnVideoPlayer.loopPointReached += StartStaticVideo;
    }

    private void StartStaticVideo(VideoPlayer video)
    {
        tvOnRawImage.SetActive(false);
        staticRawImage.SetActive(true);   
    }

    private void VideoLoadCheck(VideoPlayer video, long frame)
    {
        blackScreen.SetActive(false);
        tvOnVideoPlayer.frameReady -= VideoLoadCheck;
    }

    public void PlayVideo()
    {
        tvOnVideoPlayer.Play();
        staticVideoPlayer.Play();   
    }

    public void ReplayVideo()
    {
        tvOnRawImage.SetActive(true);

        tvOnVideoPlayer.Stop();

        staticVideoPlayer.Play();
        tvOnVideoPlayer.Play();   
    }
}
