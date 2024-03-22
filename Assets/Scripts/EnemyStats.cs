using UnityEngine;
using UnityEngine.UI;

public class EnemyStats : MonoBehaviour
{
    [Header("Stats")]
    [SerializeField] private float startHealth = 100;
    [SerializeField] private int coinsPrize = 50;
    
    public float startMovementSpeed = 10f;
    public float movementSpeed;
    public float rotationSpeed = 10f;
    private float _health;
    
    [Header("References")]
    public Transform partToRotate;
    [SerializeField] private Image healthBar;
    [SerializeField] private GameObject deathFX;
    private GameManager _gameManager;
    private AudioSource _enemyDeathSFX;

    private void Start()
    {
        _gameManager = GameObject.FindGameObjectWithTag("GameMaster").GetComponent<GameManager>();
        _enemyDeathSFX = _gameManager.enemyDeathSFX;
        _health = startHealth;
        movementSpeed = startMovementSpeed;
    }

    /// <summary>
    /// Handles enemy suffering damage 
    /// </summary>
    /// <param name="damage"></param>
    public void TakeDamage(float damage)
    {

        _health -= damage;

        healthBar.transform.parent.gameObject.SetActive(true);
        healthBar.fillAmount = _health / startHealth;
        
        if (_health <= 0)
        {
            Die();
        }
    }

    /// <summary>
    /// Used to slow the movement speed
    /// </summary>
    /// <param name="slowAmount"></param>
    public void Slow(float slowAmount)
    {
        movementSpeed = startMovementSpeed * (1f - slowAmount);
    }

    /// <summary>
    /// Handles what happens when enemy dies
    /// </summary>
    private void Die()
    {
        WaveSpawner.SpawnedEnemies--;
        _enemyDeathSFX.Play();
        PlayerStats.Coins += coinsPrize;
        var tempDeathFX = Instantiate(deathFX, transform.position, Quaternion.identity);
        Destroy(tempDeathFX,1f);
        Destroy(gameObject);
    }
}
