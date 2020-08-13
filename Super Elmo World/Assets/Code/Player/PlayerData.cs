using System;
using UnityEngine;

[Serializable]
public class PlayerData
{

    private int maxCoins = 100;
    [SerializeField] private int currentScore = 0;
    [SerializeField] private int currentCoins = 0;
    private int currentLives = 1;
    private ItemManager itemManager;

    public event Action<int, int, int> OnPlayerDataChange;

    public PlayerData(int baseCoins, int baseScore, int baseLives)
    {
        this.currentCoins = baseCoins;
        this.currentScore = baseScore;
        this.currentLives = baseLives;
    }

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

    public void LoseLives(int livesToLose)
    {
        this.currentCoins -= livesToLose;
    }

    public int GetLives()
    {
        return this.currentLives;
    }

    public void UpdateData(int coinsToAdd, int scoreToAdd, int livesToAdd)
    {
        this.currentCoins += coinsToAdd;

        // Increase lives and carry other coins over.
        if (this.currentCoins > maxCoins)
        {
            currentLives++;

            this.currentCoins -= maxCoins;
        }

        this.currentScore += scoreToAdd;
        this.currentLives += livesToAdd;

        OnPlayerDataChange(this.currentCoins, this.currentScore, this.currentLives);
    }

}
