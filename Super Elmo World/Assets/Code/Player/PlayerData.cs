using System;
using UnityEngine;

public class PlayerData : MonoBehaviour
{
    private const int maxCoins = 100;
    [SerializeField] private int currentScore = 0;
    [SerializeField] private int currentCoins = 0;
    [SerializeField] private int currentLives = 1;
    private ItemManager itemManager;
    
    public event Action<int> OnPlayerCoinsChange;
    public event Action<int> OnPlayerScoreChange;
    public event Action<int> OnPlayerLivesChange;

    public void AddCoins(int coinsToAdd)
    {
        this.currentCoins += coinsToAdd;

        // Increase lives and carry other coins over.
        if (this.currentCoins > maxCoins)
        {
            currentLives++;

            this.currentCoins -= maxCoins;
        }

        OnPlayerCoinsChange?.Invoke(this.currentCoins);
    }

    public void AddScore(int scoreToAdd)
    {
        this.currentScore += scoreToAdd;
        OnPlayerScoreChange?.Invoke(this.currentScore);
    }

    public void LoseScore(int scoreToLose)
    {
        this.currentScore -= scoreToLose;
        OnPlayerScoreChange?.Invoke(this.currentScore);
    }

    public void AddLives(int livesToAdd)
    {
        this.currentLives += livesToAdd;
        OnPlayerLivesChange?.Invoke(this.currentLives);

    }

    public void LoseLives(int livesToLose)
    {
        this.currentCoins -= livesToLose;
        OnPlayerLivesChange?.Invoke(this.currentLives);
    }

    public int GetLives()
    {
        return this.currentLives;
    }

}
