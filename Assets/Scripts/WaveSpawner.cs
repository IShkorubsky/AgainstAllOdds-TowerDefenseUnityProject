using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class WaveSpawner : MonoBehaviour
{
    public static int SpawnedEnemies;
    
    [SerializeField]private Wave [] waves;
    [SerializeField]private Transform spawnPoint;
    [SerializeField]private float timeBetweenWaves = 2f;
    [SerializeField]private TextMeshProUGUI waveText;
    [SerializeField]private TextMeshProUGUI currentWaveText;
    [SerializeField]private TextMeshProUGUI waveStartingText;
    [SerializeField]private TextMeshProUGUI nextWaveText;
    [SerializeField]private Slider waveSlider;
    [SerializeField]private TextMeshProUGUI countdownText;
    [SerializeField]private GameManager gameManager;

    private float _countdown = 2f;
    private int _waveIndex;

    private void Start()
    {
        SpawnedEnemies = 0;
        waveText.text = $"Wave: {(_waveIndex + 1).ToString()}/{waves.Length}";
        currentWaveText.text = (_waveIndex + 1).ToString();

        if (_waveIndex < waves.Length)
        {
            nextWaveText.text = (_waveIndex+2).ToString();
        }
        else
        {
            nextWaveText.text = _waveIndex.ToString();
        }
    }

    private void Update()
    {
        countdownText.text = $"{_countdown:00}";
        
        if (SpawnedEnemies > 0)
        {
            return;
        }
        
        if (_waveIndex == waves.Length && SpawnedEnemies == 0)
        {
            gameManager.gameVictorySFX.Play();
            gameManager.victoryPanel.SetActive(true);
            enabled = false;
        }

        if (_countdown <= 0f)
        {
            StartCoroutine(SpawnWave());
            _countdown = timeBetweenWaves;
            waveSlider.value = 0;
            
            waveText.text = $"Wave: {(_waveIndex + 1).ToString()}/{waves.Length}";
            currentWaveText.text = (_waveIndex + 1).ToString();
            waveStartingText.transform.parent.gameObject.SetActive(true);
            waveStartingText.text = $"Wave {_waveIndex + 1}";

            if (_waveIndex >= waves.Length) return;
            
            nextWaveText.gameObject.SetActive(true);
            nextWaveText.text = (_waveIndex+2).ToString();
            return;
        }

        _countdown -= Time.deltaTime;
        _countdown = Mathf.Clamp(_countdown, 0f, Mathf.Infinity);
    }

    /// <summary>
    /// Used to spawn a wave of enemies
    /// </summary>
    /// <returns></returns>
    private IEnumerator SpawnWave()
    {
        waveStartingText.transform.parent.gameObject.SetActive(false);
        
        waveSlider.maxValue = waves[_waveIndex].enemiesAmount;
        
        var wave = waves[_waveIndex];
        SpawnedEnemies = 0;

        for (var i = 0; i < wave.enemiesAmount; i++)
        {
            SpawnEnemy(wave.enemyPrefab);
            yield return new WaitForSeconds(1 / wave.spawnRate);
        }

        if (_waveIndex < waves.Length)
        {
            _waveIndex++;
        }
    }

    /// <summary>
    /// Used to spawn a single enemy
    /// </summary>
    /// <param name="enemyPrefab"></param>
    private void SpawnEnemy(GameObject enemyPrefab)
    {
        SpawnedEnemies++;
        waveSlider.value++;
        Instantiate(enemyPrefab, spawnPoint.position, spawnPoint.rotation);
    }
}
