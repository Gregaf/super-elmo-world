using System;
using UnityEngine;

[Serializable]
public class PlayerData
{

    private int maxCoins;
    [SerializeField] private int currentScore;
    [SerializeField] private int currentCoins;
    private int currentLives;
    private ItemManager itemManager;

    public void AddCoins(int coinsToAdd)
    {
        this.currentCoins += coinsToAdd;

        // Increase lives and carry other coins over.
        if (this.currentCoins > maxCoins)
        {
            currentLives++;

            this.currentCoins -= maxCoins;
        }
    }

    public void AddScore(int scoreToAdd)
    {
        this.currentScore += scoreToAdd;
    }

    public void LoseScore(int scoreToLose)
    {
        this.currentScore -= scoreToLose;
    }

    public void AddLives(int livesToAdd)
    {
        this.currentLives += livesToAdd;

    }

}
