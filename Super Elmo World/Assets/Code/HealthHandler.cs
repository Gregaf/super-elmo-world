using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class HealthHandler
{
    public HealthHandler()
    {
        _maxHealth = 2;
        _currentHealth = _maxHealth;
    }

    public float health { get { return _currentHealth; } }
    public float maxHealth { get { return _maxHealth; } }

    [SerializeField] private float _maxHealth;
    [SerializeField] private float _currentHealth;

    public event Action OnHealthLoss;
    public event Action OnHealthDepleted;
    
    public void AddHealth(float gainAmount)
    {
        _currentHealth += gainAmount;

        if (_currentHealth >= _maxHealth)
        {
            _currentHealth = _maxHealth;

        }
    }

    public void LoseHealth(float lossAmount)
    {
        _currentHealth -= lossAmount;

        if (_currentHealth <= 0)
        {
            _currentHealth = 0;
            OnHealthDepleted?.Invoke();
        }

        OnHealthLoss?.Invoke();
    }

    public void ResetHealth(float newMaxHealth)
    {
        _maxHealth = newMaxHealth;
        _currentHealth = newMaxHealth;
    }


}
