using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Jump : AirState
{
    private float minimumJumpTimer;

    public Jump(PlayerController playerEntity) : base(playerEntity)
    {
    }

    public override void Enter()
    {
        base.Enter();

        playerMove.dynamicGravity = playerMove.normalGravity;

        minimumJumpTimer = playerMove.minimumJumpTime;
    }

    public override void Exit()
    {
        base.Exit();

    }

    public override void Tick()
    {
        base.Tick();

        minimumJumpTimer -= Time.deltaTime;

        if (!playerInput.JumpButtonActive && minimumJumpTimer > 0)
        {
            playerEntity.velocity.y = playerMove.minimumJumpVelocity;
            playerEntity.baseMovementFSM.ChangeCurrentState(playerEntity.FallState);
        }

        if (playerEntity.velocity.y < -0.1f)
        {
            playerEntity.baseMovementFSM.ChangeCurrentState(playerEntity.FallState);
        }
    }
}

