using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BaseGame;

public class HelplessMovement : IState
{

    private CharacterController2D controller2D;
    private PlayerInputHandler playerInput;
    private Animator playerAnimator;

    private HelplessMoveProperties helplessMoveProperties;
    private List<PlayerController> activePlayers;
    private List<Transform> activePlayersPositions;
    private Transform playerPosToTrack;

    public static event Action OnAllPlayersDead;

    public HelplessMovement(HelplessMoveProperties helplessMoveProperties, CharacterController2D controller2D, PlayerInputHandler playerInput, Animator playerAnimator)
    {
        this.helplessMoveProperties = helplessMoveProperties;
        this.controller2D = controller2D;
        this.playerInput = playerInput;
        this.playerAnimator = playerAnimator;

    }

    public void Enter()
    {
        playerAnimator.SetBool("Helpless", true);
        activePlayers = GameManager.Instance.GetActivePlayers();

        Debug.Log($"Still living players: {GetTarget()}.");

        OnAllPlayersDead += StopMoving;
    }

    public void Exit()
    {
        playerAnimator.SetBool("Helpless", false);

        OnAllPlayersDead -= StopMoving;
    }


    public void Tick()
    {
        if (playerPosToTrack == null)
            return;

        // If the distance between a player is < certain distance then switch back to ground state.
        if (Vector2.Distance(playerInput.transform.position, playerPosToTrack.position) < .25f)
        {
            // Swap states.
        }
        else
        {
            playerInput.transform.position = Vector2.Lerp(playerInput.transform.position, playerPosToTrack.position, Time.deltaTime);
            
        }
    }

    private void StopMoving()
    {
        playerPosToTrack = null;
    }

    private bool GetTarget()
    {
        for(int i = 0; i < activePlayers.Count; i++)
        {
            if (activePlayers[i].isAlive)
            {
                playerPosToTrack = activePlayers[i].transform;
                return true;
            }
        }

        OnAllPlayersDead?.Invoke();
        return false;
    }

    public void OnTriggerEnter(Collider2D collider2D)
    {
        throw new NotImplementedException();
    }
}
