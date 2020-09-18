using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Jump : AirState
{
    public Jump(PlayerController playerEntity) : base(playerEntity)
    {
    }

    public override void Enter()
    {
        base.Enter();
        //SetJump();
    }

    public override void Exit()
    {
        base.Exit();

    }

    public override void Tick()
    {
        base.Tick();

        if (!playerInput.JumpButtonActive)
            playerMove.dynamicGravity = playerMove.baseGravity * playerMove.fallMultiplier;

        if (playerEntity.velocity.y < -0.1f)
        {
            playerEntity.baseMovementFSM.ChangeCurrentState(playerEntity.FallState);
        }
    }
}

