using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public enum PlayerState
{ 
    Selecting,
    Moving
}
public class WorldPlayer : MonoBehaviour
{
    public PlayerState currentPlayerState;

    private PlayerInputHandler playerInput;
    [SerializeField] private LevelMarker currentLevel;
    

    private void Awake()
    {
        playerInput = this.GetComponent<PlayerInputHandler>();

    }

    private void Update()
    {
        switch (currentPlayerState)
        {
            case PlayerState.Moving:
                break;
            case PlayerState.Selecting:
                break;
            default:
                Debug.LogError("State is empty.");
                break;
        }


    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<LevelMarker>() != null)
        {
            // access level logic.
            LevelMarker newCurrentLevel = collision.GetComponent<LevelMarker>();

            this.currentLevel = newCurrentLevel;
        }

    }

}
