using System;
using Unity.IO.LowLevel.Unsafe;
using UnityEngine;

public abstract class PickUp : MonoBehaviour
{
    [SerializeField] protected string pickUpName = "default";
    [SerializeField] protected string description = "Does some stuff.";
    [SerializeField] protected int coinValue = 0;
    [SerializeField] protected int scoreValue = 0;
    [SerializeField] protected int livesValue = 0;
    [SerializeField] protected AudioClip pickUpSound;
    protected PowerUpLifeCycle currentState;

    
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
        // If a player was touched.
        if (collider2D.GetComponent<PlayerBrain>() != null)
        {
            PlayerInputHandler player = collider2D.GetComponent<PlayerInputHandler>();

            GameManager.Instance.eachPlayersData[player.playerIndex].UpdateData(coinValue, scoreValue, livesValue);

            // Review: Will I have every object destroy itself? May come back for object pooling, though most likely unneccessary.
            Destroy(this.gameObject);
        }
    }

}

