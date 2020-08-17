using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HelplessMovement : IState
{

    private CharacterController2D controller2D;
    private PlayerInputHandler playerInput;
    private Animator playerAnimator;

    private HelplessMoveProperties helplessMoveProperties;
    private List<PlayerBrain> activePlayers;
    private Vector2 playerPosToTrack;

    public static event Action OnPlayerRespawned;

    public HelplessMovement(HelplessMoveProperties helplessMoveProperties, CharacterController2D controller2D, PlayerInputHandler playerInput, Animator playerAnimator)
    {
        this.helplessMoveProperties = helplessMoveProperties;
        this.controller2D = controller2D;
        this.playerInput = playerInput;
        this.playerAnimator = playerAnimator;

        this.activePlayers = GameManager.Instance.GetActivePlayers();
    }

    public void Enter()
    {
        playerAnimator.SetBool("Helpless", true);
        playerPosToTrack = GetTarget();
    }

    public void Exit()
    {
        playerAnimator.SetBool("Helpless", false);
        OnPlayerRespawned?.Invoke();
    }


    public void StateUpdate()
    {
        // If the distance between a player is < certain distance then switch back to ground state.
        playerInput.transform.position = Vector2.Lerp(playerInput.transform.position, playerPosToTrack, Time.deltaTime);
        Debug.Log(playerPosToTrack);   
    }

    private Vector2 GetTarget()
    {
        for(int i = 0; i < activePlayers.Count; i++)
        {
            if (activePlayers[i].isAlive)
                return activePlayers[i].transform.position;
        }

        
        return Vector2.zero;
    }
}
