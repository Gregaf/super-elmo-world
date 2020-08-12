using System;
using Unity.IO.LowLevel.Unsafe;
using UnityEngine;

public abstract class PickUp : MonoBehaviour
{
    protected string pickUpName = "default";
    protected string description = "Does some stuff.";
    protected PickUpEventArgs pickUpEventArgs;
    protected AudioClip pickUpSound;
    protected PowerUpLifeCycle currentState;

    public EventHandler<PickUpEventArgs> pickUpEventHandler;

    public enum PowerUpLifeCycle
    {
        AttractionState,
        PayloadState,
        ExpirationState
    }

    protected virtual void Start()
    {
        currentState = PowerUpLifeCycle.AttractionState;
        pickUpEventArgs = new PickUpEventArgs();
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
        // If a player was touched.
        if (collider2D.GetComponent<PlayerBrain>() != null)
        {
            // Play sound effect.
            // Pick up particles.
            pickUpEventHandler?.Invoke(this, pickUpEventArgs);
            // Review: Will I have every object destroy itself? May come back for object pooling, though most likely unneccessary.
            Destroy(this.gameObject);
        }
    }

}

