using System;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class MainMenuManager : MonoBehaviour
{
    [SerializeField]private GameObject mainMenuPanel;
    [SerializeField]private GameObject levelSelectionPanel;
    [SerializeField]private GameObject settingsMenuPanel;
    [SerializeField]private SceneFading sceneFading;

    [SerializeField]private Button[] levelButtons;
    [SerializeField]private GameObject[] levelLocks;
    [SerializeField]private AudioMixer audioMixer;
    [SerializeField]private AudioSource uiClickSFX;

    private void Start()
    {
        var savedLevel = 0;
        savedLevel = PlayerPrefs.GetInt("levelReachedSave", savedLevel);
        Debug.Log("Current level save:" + savedLevel);

        SetVolumeParameters(30f);

        if (!Screen.fullScreen)
        {
            Screen.fullScreenMode = FullScreenMode.ExclusiveFullScreen;
        }
    }

    private void Update()
    {
        var levelReached = PlayerPrefs.GetInt("levelReachedSave", 1);

        for (var i = 0; i < levelButtons.Length; i++)
        {
            if (i + 1 > levelReached)
            {
                levelButtons[i].interactable = false;
                levelLocks[i].gameObject.SetActive(true);
            }
        }

        for (var i = 0; i < levelReached; i++)
        {
            levelLocks[i].gameObject.SetActive(false);
        }
    }

    #region Levels

    /// <summary>
    /// Used to load Level1
    /// </summary>
    public void LoadLevel1()
    {        
        uiClickSFX.Play();
        sceneFading.FadeToAnotherScene(1);
    }
    
    /// <summary>
    /// Used to load Level2
    /// </summary>
    public void LoadLevel2()
    {
        uiClickSFX.Play();
        sceneFading.FadeToAnotherScene(2);
    }
    
    /// <summary>
    /// Used to load Level3
    /// </summary>
    public void LoadLevel3()
    {
        uiClickSFX.Play();
        sceneFading.FadeToAnotherScene(3);
    }
    
    /// <summary>
    /// Used to load Level4
    /// </summary>
    public void LoadLevel4()
    {
        uiClickSFX.Play();
        sceneFading.FadeToAnotherScene(4);
    }
    
    /// <summary>
    /// Used to load Level5
    /// </summary>
    public void LoadLevel5()
    {
        uiClickSFX.Play();
        sceneFading.FadeToAnotherScene(5);
    }

    #endregion
    
    /// <summary>
    /// Used to show main menu screen
    /// </summary>
    public void ShowMainMenu()
    {
        uiClickSFX.Play();
        settingsMenuPanel.SetActive(false);
        levelSelectionPanel.SetActive(false);
        mainMenuPanel.SetActive(true);
    }

    private void SetVolumeParameters(float multiplierValue)
    {
        float masterVolume = 0f;
        masterVolume = PlayerPrefs.GetFloat("MasterVolume", masterVolume);
        float musicVolume = 0f;
        musicVolume = PlayerPrefs.GetFloat("MusicVolume", musicVolume);
        float SoundFxVolume = 0f;
        SoundFxVolume = PlayerPrefs.GetFloat("SoundFXVolume", SoundFxVolume);

        audioMixer.SetFloat("MasterVolume", Mathf.Log10(masterVolume) * multiplierValue);
        audioMixer.SetFloat("MusicVolume", Mathf.Log10(musicVolume) * multiplierValue);
        audioMixer.SetFloat("SoundFXVolume", Mathf.Log10(SoundFxVolume) * multiplierValue);
    }

    /// <summary>
    /// Used to show the level selection screen
    /// </summary>
    public void ShowLevelSelectionPanel()
    {
        uiClickSFX.Play();
        settingsMenuPanel.SetActive(false);
        mainMenuPanel.SetActive(false);
        levelSelectionPanel.SetActive(true);
    }
    
    /// <summary>
    /// Used to show the settingss menu
    /// </summary>
    public void EnableSettingsMenu()
    {
        uiClickSFX.Play();
        levelSelectionPanel.SetActive(false);
        mainMenuPanel.SetActive(false);
        settingsMenuPanel.SetActive(true);
    }

    public void DeleteAllPlayerPrefs()
    {
        PlayerPrefs.DeleteAll();
    }
}
