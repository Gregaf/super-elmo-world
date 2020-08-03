using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthScript : MonoBehaviour
{
    [SerializeField] private int maxHealth = 3;
    public int currentHealth { get; private set; }
    
    public event EventHandler OnGainHealthCallback;
    public event EventHandler OnLoseHealthCallback;

    public void GainHealth(int gainAmount)
    {
        currentHealth += gainAmount;

        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);
       
        // Raise event when health is gained.      
        OnGainHealthCallback?.Invoke(this, EventArgs.Empty);

    }

    public void LoseHealth(int lossAmount)
    {      
        currentHealth -= lossAmount;
  
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);

        // Riase event when health is lost.
        OnLoseHealthCallback?.Invoke(this, EventArgs.Empty);
    }

}
