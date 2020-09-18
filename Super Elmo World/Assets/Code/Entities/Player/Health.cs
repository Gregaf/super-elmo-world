using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    private int maxHealth = 3;

    public int currentHealth { get; private set; }

    public event System.Action OnHealthLoss;
    public event System.Action OnEntityDeath;

    public void AddHealth(int newHealth)
    {
        currentHealth += newHealth;

        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);
    }

    public void LoseHealth(int lossAmount)
    {

        currentHealth -= lossAmount;

        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);

        if (currentHealth <= 0)
        {
            OnEntityDeath?.Invoke();

        }
        else
            OnHealthLoss?.Invoke();
    }


}
