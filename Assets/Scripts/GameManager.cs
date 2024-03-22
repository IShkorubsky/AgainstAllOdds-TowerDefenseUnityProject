using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    private bool _gameOver;
    
    [SerializeField]private GameObject gameOverPanel;
    [SerializeField]private GameObject pauseMenuPanel;
    [SerializeField]private TMP_Text currentLevelText;
    [SerializeField]private GameObject settingsMenuPanel;
    [SerializeField]private SceneFading sceneFading;
    [SerializeField]private AudioSource uiClickSFX;
    [SerializeField]private AudioSource gameOverSFX;
    [SerializeField]private int nextLevel = 2;

    public GameObject victoryPanel;
    public AudioSource gameVictorySFX;
    public AudioSource enemyDeathSFX;


    private void Start()
    {
        Time.timeScale = 1;
        _gameOver = false;
    }
    
    private void Update()
    {
        if (_gameOver)
        {
            return;
        }
        
        if (PlayerStats.Lives <= 0)
        {
            GameOver();
        }
    }

    /// <summary>
    /// Handles showing game over screen
    /// </summary>
    private void GameOver()
    {
        gameOverSFX.Play();
        _gameOver = true;
        gameOverPanel.SetActive(true);
    }

    /// <summary>
    /// Handles next level button on victory screen
    /// </summary>
    public void WinLevel()
    {
        PlayerPrefs.SetInt("levelReachedSave",nextLevel);
        sceneFading.FadeToAnotherScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    /// <summary>
    /// Used to display the pause menu
    /// </summary>
    public void PauseGame()
    {
        uiClickSFX.Play();
        currentLevelText.text = "Chapter 1-" + (nextLevel - 1);
        Time.timeScale = 0;
        pauseMenuPanel.SetActive(true);
    }

    /// <summary>
    /// Used to unpause the game
    /// </summary>
    public void UnpauseGame()
    {
        uiClickSFX.Play();
        Time.timeScale = 1;
        pauseMenuPanel.SetActive(false);
    }
    
    /// <summary>
    /// Used to go to the main menu scene
    /// </summary>
    public void GoToMainMenu()
    {
        uiClickSFX.Play();
        Time.timeScale = 1;
        sceneFading.FadeToAnotherScene(0);
    }
    
    /// <summary>
    /// Used to restart the current level
    /// </summary>
    public void RestartLevel()
    {
        uiClickSFX.Play();
        Time.timeScale = 1;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    
    /// <summary>
    /// Used to enable the settings screen
    /// </summary>
    public void EnableSettingsMenu()
    {
        uiClickSFX.Play();
        pauseMenuPanel.SetActive(false);
        settingsMenuPanel.SetActive(true);
    }

    /// <summary>
    /// Used to return to the pause menu from settings
    /// </summary>
    public void ReturnToPauseMenu()
    {
        uiClickSFX.Play();
        settingsMenuPanel.SetActive(false);
        pauseMenuPanel.SetActive(true);
    }
}
