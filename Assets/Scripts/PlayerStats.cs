using TMPro;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    public static int Coins;
    [SerializeField] private int startCoins = 400;
    [SerializeField] private TextMeshProUGUI coinsText;

    public static int Lives;
    [SerializeField] private int startLives = 20;
    [SerializeField] private TextMeshProUGUI livesText;
    
    private void Start()
    {
        Coins = startCoins;
        Lives = startLives;
    }

    private void Update()
    {
        coinsText.text = Coins.ToString();
        livesText.text = Lives.ToString();
    }
}
