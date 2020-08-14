using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthManager : MonoBehaviour
{

    private const int maxHealth = 3;
    public int currentHealth {get; private set;}

    public event Action OnEntityDie;
    public event EventHandler<GrowthEventArgs> OnHealthChange;

    private GrowthEventArgs growthEventArgs;
    private PowerUp lastPickedUp;

    public void Start()
    {
        growthEventArgs = new GrowthEventArgs(currentHealth);
        currentHealth = 1;

    }


    public void SetHealthAndID(int newHealth, int growthID)
    {
        if (currentHealth > newHealth)
        {
            // Item should be stored.
            return;
        }

        currentHealth = newHealth;

        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);

        growthEventArgs.currentHealth = this.currentHealth;
        growthEventArgs.growthID = growthID;

        OnHealthChange?.Invoke(this, growthEventArgs);
    }

    public void LoseHealth(int amountToLose)
    {
        currentHealth -= amountToLose;

        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);

        if (currentHealth <= 0 && OnEntityDie != null)
            OnEntityDie();

        growthEventArgs.currentHealth = this.currentHealth;

        OnHealthChange?.Invoke(this, growthEventArgs);
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<PowerUp>() != null)
        {
            lastPickedUp = collision.GetComponent<PowerUp>();

            
        }
    }

}
