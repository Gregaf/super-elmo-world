using SMTest;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Idle : GroundState
{
    float storeFloat;

    public Idle(PlayerController playerEntity) : base(playerEntity)
    {
    }

    public override void Enter()
    {
        base.Enter();
    }

    public override void Exit()
    {
        base.Exit();

    }

    public override void Tick()
    {
        base.Tick();

        //if (playerInput.MovementInput.y == 1)
        //    locomotionFsm.ChangeCurrentState((int) PlayerGroundStates.BACKWALLCLIMBING);

        // Need to multiply by Acceleration.
        //controller2D.SetHorizontalForce(Mathf.Lerp(controller2D.Velocity.x, 0, Time.deltaTime * playerMove.groundAcceleration));
        playerEntity.velocity.x = Mathf.SmoothDamp(playerEntity.velocity.x, 0, ref storeFloat, 0.25f);

        if (playerInput.IsMovingHorizontally)
        {
            playerEntity.baseMovementFSM.ChangeCurrentState(playerEntity.MoveState);

        }
    }
}

