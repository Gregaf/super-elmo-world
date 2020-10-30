using UnityEngine.UI;
using UnityEngine;
using TMPro;
using System;

namespace BaseGame
{
    public class PlayerInfoUI : MonoBehaviour
    {
        [SerializeField] Image playerImage;
        [SerializeField] TextMeshProUGUI coins, score, lives;

        private void Awake()
        {
            playerImage = transform.GetChild(0).GetComponent<Image>();
            coins = transform.GetChild(1).GetComponent<TextMeshProUGUI>();
            score = transform.GetChild(2).GetComponent<TextMeshProUGUI>();
            lives = transform.GetChild(3).GetComponent<TextMeshProUGUI>();

        }

        private void OnEnable()
        {

        }

        private void OnDisable()
        {
 
        }

        public void UpdateCoinsText(int newCoins)
        {
            coins.text = $"Coins:{newCoins}";

        }
        public void UpdateScoreText(int newScore)
        {
            score.text = $"Score:{newScore}";

        }
        public void UpdateLivesText(int newLives)
        {
            lives.text = $"Lives:{newLives}";
        }

    }
}