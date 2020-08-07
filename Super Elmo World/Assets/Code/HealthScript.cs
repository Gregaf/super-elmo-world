using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthScript : MonoBehaviour
{
    [SerializeField] private int maxHealth = 3;
    public int currentHealth { get; private set; }
    
    public event EventHandler<GrowthEventArgs> OnLoseHealthCallback;
    public event EventHandler OnDeathCallback;

    private GrowthEventArgs growthArguments;

    public void Start()
    {
        currentHealth = 2;

        growthArguments = new GrowthEventArgs(currentHealth);
    }

    public void LoseHealth(int lossAmount)
    {
        currentHealth -= lossAmount;
  
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);

        growthArguments.growthLevel = currentHealth;
        growthArguments.SetGrowthState();

        // Riase event when health is lost.
        OnLoseHealthCallback?.Invoke(this, growthArguments);

        if (currentHealth <= 0)
            OnDeathCallback?.Invoke(this, EventArgs.Empty);
    }

}
