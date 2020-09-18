using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class AirState : PlayerState
{
    private float wallJumpTimer;
    private float wallJumpWaitTime = 0.05f;

    public AirState(PlayerController playerEntity) : base(playerEntity)
    {

    }

    public override void Enter()
    {
        if (playerEntity.baseMovementFSM.PreviousState == playerEntity.WallSlideState)
        {
            wallJumpTimer = wallJumpWaitTime;
        }

    }

    public override void Exit()
    {

    }

    public override void Tick()
    {
        wallJumpTimer -= Time.deltaTime;

        playerEntity.velocity.x = Mathf.Lerp(playerEntity.velocity.x, playerInput.MovementInput.x * playerMove.currentSpeed, Time.deltaTime * playerMove.airAcceleration);
        playerEntity.velocity.y += -(playerMove.dynamicGravity * Time.deltaTime);
        //controller2D.SetHorizontalForce(Mathf.Lerp(controller2D.Velocity.x, playerInput.MovementInput.x * playerMove.currentSpeed, Time.deltaTime * playerMove.airAcceleration));
        //controller2D.SetVerticalForce(Mathf.Lerp(controller2D.Velocity.y, -playerMove.dynamicGravity, Time.deltaTime));

        //if (playerEntity.TouchingWall((int)playerInput.MovementInput.x) && wallJumpTimer < 0)
        if(WallJumpCol() && wallJumpTimer < 0)
            playerEntity.baseMovementFSM.ChangeCurrentState(playerEntity.WallSlideState);

        if (playerInput.MovementInput.y == -1)
        {
            if (playerInput.JumpButtonActive)
                playerEntity.baseMovementFSM.ChangeCurrentState(playerEntity.GroundPoundState);

        }
    }

    public bool WallJumpCol()
    {
        if (controller2D.collisionInfo.left && playerInput.MovementInput.x == -1)
            return true;
        else if (controller2D.collisionInfo.right && playerInput.MovementInput.x == 1)
            return true;
        else
            return false; 
    }

}
