using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class AirState : PlayerState
{
    
    private float storeFloat = 0;

    public AirState(PlayerController playerEntity) : base(playerEntity)
    {

    }

    public override void Enter()
    {
    }

    public override void Exit()
    {

    }

    public override void Tick()
    {
        //playerEntity.velocity.x = Mathf.Lerp(playerEntity.velocity.x, playerInput.MovementInput.x * playerMove.currentSpeed, Time.deltaTime * playerMove.airAcceleration);
        //playerEntity.velocity.y += -(playerMove.dynamicGravity * Time.deltaTime);
        //controller2D.SetHorizontalForce(Mathf.Lerp(controller2D.Velocity.x, playerInput.MovementInput.x * playerMove.currentSpeed, Time.deltaTime * playerMove.airAcceleration));
        //controller2D.SetVerticalForce(Mathf.Lerp(controller2D.Velocity.y, -playerMove.dynamicGravity, Time.deltaTime));

        //if (playerEntity.TouchingWall((int)playerInput.MovementInput.x) && wallJumpTimer < 0)
        playerEntity.velocity.x = Mathf.SmoothDamp(playerEntity.velocity.x, playerMove.currentSpeed * playerInput.MovementInput.x, ref storeFloat, playerMove.airAccelerationTime);
        playerEntity.velocity.y += playerMove.dynamicGravity * Time.deltaTime;

        if (playerInput.MovementInput.y == -1)
        {
            if (playerInput.JumpButtonActive)
                playerEntity.baseMovementFSM.ChangeCurrentState(playerEntity.GroundPoundState);

        }
    }

}
