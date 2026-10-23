using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class SoundMixerManager : MonoBehaviour
{

    [SerializeField] private AudioMixer audioMixer;
    [SerializeField] private Slider masterSlider;
    [SerializeField] private Slider sfxSlider;
    [SerializeField] private Slider musicSlider;

    public void Start()
    {
        if (PlayerPrefs.HasKey("masterVolume"))
        {
            LoadMaster();
        }
        else
        {
            SetMasterVolume(masterSlider.value);
        }

        if (PlayerPrefs.HasKey("sfxVolume"))
        {
            LoadSFX();
        }
        else
        {
            SetSFXVolume(sfxSlider.value);
        }

        if (PlayerPrefs.HasKey("musicVolume"))
        {
            LoadMusic();
        }
        else
        {
            SetMusicVolume(musicSlider.value);
        }
    }
    public void SetMasterVolume (float volume)
    {   
        audioMixer.SetFloat("masterVolume", Mathf.Log10(volume) * 20f);
        PlayerPrefs.SetFloat("masterVolume", volume);
    }

    public void SetSFXVolume (float volume)
    {
        audioMixer.SetFloat("sfxVolume", Mathf.Log10(volume) * 20f);
        PlayerPrefs.SetFloat("sfxVolume", volume);
    }

    public void SetMusicVolume (float volume)
    {
        audioMixer.SetFloat("musicVolume", Mathf.Log10(volume) * 20f);
        PlayerPrefs.SetFloat("musicVolume", volume);
    }
    
    private void LoadMaster()
    {
        masterSlider.value = PlayerPrefs.GetFloat("masterVolume");
        SetMasterVolume(masterSlider.value);
    }

    private void LoadSFX()
    {
        sfxSlider.value = PlayerPrefs.GetFloat("sfxVolume");
        SetSFXVolume(sfxSlider.value);
    }

    private void LoadMusic()
    {
        musicSlider.value = PlayerPrefs.GetFloat("musicVolume");
        SetMusicVolume(musicSlider.value);
    }
}
