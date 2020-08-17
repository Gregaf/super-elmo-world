using UnityEngine.UI;
using UnityEngine;
using TMPro;
using System;

public class PlayerInfoUI : MonoBehaviour
{
    [SerializeField] private int playerIndex = 0;
    [SerializeField] Image playerImage;
    [SerializeField] TextMeshProUGUI coins, score, lives;



    private void Awake()
    {
        playerImage = transform.GetChild(0).GetComponent<Image>();
        coins = transform.GetChild(1).GetComponent<TextMeshProUGUI>();
        score = transform.GetChild(2).GetComponent<TextMeshProUGUI>();
        lives = transform.GetChild(3).GetComponent<TextMeshProUGUI>();

        UpdateUIText(0, 0, 0);
    }

    private void OnEnable()
    {

    }

    private void OnDisable()
    {
    }

    public void UpdateUIText(int newCoins, int newScore, int newLives)
    {
        coins.text = $"Coins:{newCoins}";
        score.text = $"Score:{newScore}";
        lives.text = $"Lives:{newLives}";
    }

}
