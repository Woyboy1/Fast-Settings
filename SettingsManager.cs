using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class SettingsManager : MonoBehaviour
{
    [Header("Canvas")]
    [SerializeField] private GameObject canvas;

    [Header("Information")]
    [SerializeField] private GameObject gameInformation;
    [SerializeField] private GameObject controlsInformation;
    [SerializeField] private GameObject graphicsInformation;

    [Space(10)]

    [SerializeField] private TMP_Dropdown resolutionDropdown;

    [Header("Audio")]
    [SerializeField] private AudioMixer audioMixer;

    Resolution[] resolutions;
    AudioSource source;

    private void Awake()
    {
        gameInformation.SetActive(false);
        controlsInformation.SetActive(false);
        graphicsInformation.SetActive(false);

        source = GetComponent<AudioSource>();
    }
    private void Start()
    {
        resolutions = Screen.resolutions;
        resolutionDropdown.ClearOptions();

        List<string> options = new List<string>();
        int currentResolutionIndex = 0;
        for (int i = 0; i < resolutions.Length; i++)
        {
            string option = resolutions[i].width + " x " + resolutions[i].height;
            options.Add(option);

            if (resolutions[i].width == Screen.currentResolution.width && resolutions[i].height == Screen.currentResolution.height)
            {
                currentResolutionIndex = i;
            }
        }
        resolutionDropdown.AddOptions(options);
        resolutionDropdown.value = currentResolutionIndex;
        resolutionDropdown.RefreshShownValue();
    }

    #region Buttons
    public void DisplayGameInformation()
    {
        PlaySound();
        gameInformation.SetActive(true);
        controlsInformation.SetActive(false);
        graphicsInformation.SetActive(false);
    }

    public void DisplayControlsInformation()
    {
        PlaySound();
        gameInformation.SetActive(false);
        controlsInformation.SetActive(true);
        graphicsInformation.SetActive(false);
    }

    public void DisplayGraphicsInformation()
    {
        PlaySound();
        gameInformation.SetActive(false);
        controlsInformation.SetActive(false);
        graphicsInformation.SetActive(true);
    }

    public void ReturnButton()
    {
        PlaySound();
        gameInformation.SetActive(false);
        controlsInformation.SetActive(false);
        graphicsInformation.SetActive(false);
        canvas.SetActive(false);
    }
    #endregion

    #region Information

    #region GameInformation / Volume Information

    public void SetMasterVolume(float volume)
    {
        audioMixer.SetFloat("MasterVolume", volume);
        PlaySound();
    }

    public void SetMusicVolume(float volume)
    {
        audioMixer.SetFloat("MusicVolume", volume);
        PlaySound();
    }

    public void SetSFXVolume(float volume)
    {
        audioMixer.SetFloat("SFXVolume", volume);
        PlaySound();
    }
    #endregion

    #region Controls Information

    // Write this code manually.

    #endregion

    #region Graphics Information

    public void SetResolution(int resolutionIndex)
    {
        PlaySound();
        Resolution resolution = resolutions[resolutionIndex];
        Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreen);
    }

    public void SetFullscreen(int index)
    {
        PlaySound();
        if (index == 0)
            Screen.fullScreen = true;
        else if (index == 1)
            Screen.fullScreen = false;
        else
            Debug.LogWarning("FULLSCREEN ERROR - VALUES CHANGED");
    }

    public void SetQuality(int quality)
    {
        PlaySound();
        QualitySettings.SetQualityLevel(quality);
    }

    #endregion

    #region SFX

    void PlaySound()
    {
        source.PlayOneShot(source.clip);
    }

    #endregion

    #endregion
}
