using System;
using Unity.IO.LowLevel.Unsafe;
using UnityEngine;

public abstract class PowerUp : MonoBehaviour
{
    protected string powerUpName = "default";
    protected string description = "Does some stuff.";
    protected GrowthEventArgs powerUpEventArgs;
    protected AudioClip pickUpSound;
    protected PowerUpLifeCycle currentState;

    public EventHandler<GrowthEventArgs> pickUpEventHandler;

    public enum PowerUpLifeCycle
    {
        AttractionState,
        PayloadState,
        ExpirationState
    }

    protected virtual void Start()
    {
        currentState = PowerUpLifeCycle.AttractionState;

    }

    protected virtual void Update()
    {
        switch (currentState)
        {
            case PowerUpLifeCycle.AttractionState:
                AttractionState();
                break;
            case PowerUpLifeCycle.PayloadState:
                // Run PayloadState logic
                break;
            case PowerUpLifeCycle.ExpirationState:
                // Run Expiration state logic.
                break;
            default:
                break;
        }
    }

    protected virtual void AttractionState()
    {

    }

    protected virtual void PayLoadState()
    {

    }

    protected virtual void ExpirationState()      
    {
        
    }

    protected virtual void OnTriggerEnter2D(Collider2D collider2D)
    {

    }

}

