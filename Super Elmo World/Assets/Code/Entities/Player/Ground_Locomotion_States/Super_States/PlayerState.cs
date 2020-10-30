using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerState : State
{
    protected PlayerController playerEntity;
    protected PlayerInputHandler playerInput;
    protected Controller2D controller2D;
    protected PlayerMoveProperties playerMove;
    //protected FSM locomotionFsm;

    public PlayerState(PlayerController playerEntity)
    {
        this.playerEntity = playerEntity;

        playerInput = playerEntity.PlayerInputHandler;
        controller2D = playerEntity.Control2D;
        playerMove = playerEntity.playerMoveProperties;
    }


    public override void Enter()
    {

    }

    public override void Exit()
    {

    }

    public override void OnTriggerEnter2D(Collider2D collider2D)
    {
    }

    public override void Tick()
    {

    }
}
