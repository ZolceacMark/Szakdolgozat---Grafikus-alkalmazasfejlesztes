using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SettingsMenu : MonoBehaviour
{
    public TMPro.TMP_Dropdown resolutionDropdown;
    Resolution[] resolutions; 
    List<Resolution> filteredResolutions = new List<Resolution>();
    float currentRefreshRate;
    int currentResolutionIndex = 0;
    [SerializeField] private Toggle fullScreen;

    FullScreenMode GetToggle(){
            if (PlayerPrefs.GetInt("FullScreen", 1) == 1)
            {
                return FullScreenMode.FullScreenWindow;
                //fullScreen.isOn = true;
            }
            else
            {
                return FullScreenMode.Windowed;
                //fullScreen.isOn = false;
            }
        }

    void Start()
    {
        resolutions = Screen.resolutions;

        resolutionDropdown.ClearOptions();

        currentRefreshRate = (float)Screen.currentResolution.refreshRateRatio.value;

        fullScreen.isOn = PlayerPrefs.GetInt("FullScreen", 1) == 1;

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
            string option = filteredResolutions[i].width + "x" + 
                            filteredResolutions[i].height + " " + Mathf.Round((float)filteredResolutions[i].refreshRateRatio.value) + "Hz";

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
        Screen.SetResolution(resolution.width, resolution.height, GetToggle(), resolution.refreshRateRatio);
    }

    public void SetFullScreen(bool isFullScreen)
    {
        PlayerPrefs.SetInt("FullScreen", isFullScreen ? 1 : 0);
        PlayerPrefs.Save();
        Screen.fullScreenMode = GetToggle();
    }

    public void SetVolume (float mainVolume)
        {
            PlayerPrefs.SetFloat("mainVolume", mainVolume);
            PlayerPrefs.Save(); 
        }
}
