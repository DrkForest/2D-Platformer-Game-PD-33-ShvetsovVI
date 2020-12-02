using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class SettingsMenuController : MonoBehaviour
{
    [Header("Volume")]
    [SerializeField] private Slider volume;
    [SerializeField] private AudioMixer masterMixer;

    [Space]
    [SerializeField] private Toggle fullScreen;
    [Space]
    [SerializeField] TMP_Dropdown resolutionDropdown;
    private Resolution[] avaibleResolution;
    [Space]
    [SerializeField] TMP_Dropdown qualityDropdown;
    private string[] qualityLevels;
    
    // Start is called before the first frame update
    void Start()
    {
        volume.onValueChanged.AddListener(OnVolumeChange);
        fullScreen.onValueChanged.AddListener(OnFullScreenChanged);
        resolutionDropdown.onValueChanged.AddListener(OnResolutionChanged);

        qualityDropdown.onValueChanged.AddListener(OnQualityChanged);
        avaibleResolution = Screen.resolutions;
        resolutionDropdown.ClearOptions();
        int currentIndex = 0;
        List<string> options = new List<string>();
        for(int i =0; i<avaibleResolution.Length; i++)
        {
            if(avaibleResolution[i].width <= 800)
            {
                continue;
            }
            options.Add(avaibleResolution[i].width + "x" + avaibleResolution[i].height);
            if (avaibleResolution[i].width == Screen.currentResolution.width && avaibleResolution[i].height == Screen.currentResolution.height)
            {
                currentIndex = 1;
            }
        }
        resolutionDropdown.AddOptions(options);
        resolutionDropdown.value = currentIndex;
        resolutionDropdown.RefreshShownValue();
        qualityLevels = QualitySettings.names;
        qualityDropdown.ClearOptions();
        qualityDropdown.AddOptions(qualityLevels.ToList());
        int qualityLvl    = QualitySettings.GetQualityLevel();
        qualityDropdown.value = qualityLvl;
        qualityDropdown.RefreshShownValue();
    }

    private void OnDestroy()
    {
        volume.onValueChanged.RemoveListener(OnVolumeChange);
        fullScreen.onValueChanged.RemoveListener(OnFullScreenChanged);
        resolutionDropdown.onValueChanged.RemoveListener(OnResolutionChanged);
        qualityDropdown.onValueChanged.RemoveListener(OnQualityChanged);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnVolumeChange(float volume)
    {
        masterMixer.SetFloat("MyExposedParam", volume);
    }
    private void OnFullScreenChanged(bool value)
    {
        Screen.fullScreen = value;
    }

    private void OnResolutionChanged(int resolutionIndex)
    {
        Resolution resolution = avaibleResolution[resolutionIndex];
        Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreen);
    }
    private void OnQualityChanged( int qualityLvl)
    {
        QualitySettings.SetQualityLevel(qualityLvl, true);
    }
}
