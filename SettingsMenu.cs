using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class SettingsMenu : MonoBehaviour
{
    //public AudioMixer audioMixer;
    //[SerializeField] private Slider volumeSlider;
    public TMPro.TMP_Dropdown resolutionDropdown;
    Resolution[] resolutions; 
    List<Resolution> filteredResolutions = new List<Resolution>();
    float currentRefreshRate;
    int currentResolutionIndex = 0;

    void Start()
    {
        resolutions = Screen.resolutions;

        resolutionDropdown.ClearOptions();

        currentRefreshRate = (float)Screen.currentResolution.refreshRateRatio.value;
        
        List<string> options = new List<string>();

        for (int i = 0; i < resolutions.Length; i++)
        {  
            if (Mathf.Abs((float)resolutions[i].refreshRateRatio.value - currentRefreshRate) < 0.1f)
            {
                filteredResolutions.Add(resolutions[i]);
            }
        }
            
        for (int i = 0; i < filteredResolutions.Count; i++)
        {
            string option = filteredResolutions[i].width + "x" + filteredResolutions[i].height + " " + filteredResolutions[i].refreshRateRatio + "Hz";
            options.Add(option);

            if (filteredResolutions[i].width == Screen.width &&
                filteredResolutions[i].height == Screen.height)
            {
                currentResolutionIndex = i;
            }
        }

        resolutionDropdown.AddOptions(options);
        resolutionDropdown.value = currentResolutionIndex;
        resolutionDropdown.RefreshShownValue();
    }

    public void SetResolution (int resolutionIndex)
    {
        Resolution resolution = filteredResolutions[resolutionIndex];
        Screen.SetResolution(resolution.width, resolution.height, true);
    }

    public void SetVolume (float mainVolume)
        {
            //audioMixer.SetFloat("mainVolume", mainVolume);
            PlayerPrefs.SetFloat("mainVolume", mainVolume);
            PlayerPrefs.Save();
            Debug.Log(mainVolume);  
        }

    public void SetFullScreen(bool isFullScreen)
    {
        Screen.fullScreen = isFullScreen;
        Debug.Log("FS Toggle");
        PlayerPrefs.SetInt("FullScreen", isFullScreen ? 1 : 0);
        PlayerPrefs.Save();
    }

}
