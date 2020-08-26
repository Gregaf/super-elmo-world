using UnityEngine.UI;
using UnityEngine;
using TMPro;
using System;

namespace BaseGame
{
    public class PlayerInfoUI : MonoBehaviour
    {
        [SerializeField] private int playerIndex = 0;
        [SerializeField] Image playerImage;
        [SerializeField] TextMeshProUGUI coins, score, lives;

        private PlayerController player;


        private void Awake()
        {
            playerImage = transform.GetChild(0).GetComponent<Image>();
            coins = transform.GetChild(1).GetComponent<TextMeshProUGUI>();
            score = transform.GetChild(2).GetComponent<TextMeshProUGUI>();
            lives = transform.GetChild(3).GetComponent<TextMeshProUGUI>();

        }

        private void OnEnable()
        {
            PlayerController[] allPlayerData = FindObjectsOfType<PlayerController>();

            for (int i = 0; i < allPlayerData.Length; i++)
            {
                if (allPlayerData[i].playerInput.playerIndex == playerIndex)
                    player = allPlayerData[i];
            }

            
            player.playerData.OnPlayerCoinsChange += UpdateCoinsText;
            player.playerData.OnPlayerScoreChange += UpdateScoreText;
            player.playerData.OnPlayerLivesChange += UpdateLivesText;
        }

        private void OnDisable()
        {

            player.playerData.OnPlayerCoinsChange -= UpdateCoinsText;
            player.playerData.OnPlayerScoreChange -= UpdateScoreText;
            player.playerData.OnPlayerLivesChange -= UpdateLivesText;
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