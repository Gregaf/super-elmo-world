using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Fall : AirState
{
    private float graceTimer = 0;

    private float wallJumpTimer;
    private float wallJumpWaitTime = 0.05f;

    public Fall(PlayerController playerEntity) : base(playerEntity)
    {
    }

    public override void Enter()
    {
        base.Enter();

        if (playerEntity.baseMovementFSM.PreviousState == playerEntity.WallSlideState)
        {
            wallJumpTimer = wallJumpWaitTime;
        }

        playerMove.dynamicGravity = playerMove.normalGravity * playerMove.gravityScale;

        playerInput.playerControls.Basic.Jump.performed += JumpTransition;

        controller2D.OnFellEvent += ResetGraceTime;
    }

    public override void Exit()
    {
        base.Exit();

        playerInput.playerControls.Basic.Jump.performed -= JumpTransition;

        controller2D.OnFellEvent -= ResetGraceTime;
    }

    public override void Tick()
    {
        base.Tick();

        wallJumpTimer -= Time.deltaTime;

        graceTimer -= Time.deltaTime;

        if (WallJumpCol() && wallJumpTimer < 0)
            playerEntity.baseMovementFSM.ChangeCurrentState(playerEntity.WallSlideState);

        if (controller2D.collisionInfo.isGrounded)
        {
            playerEntity.baseMovementFSM.ChangeCurrentState(playerEntity.IdleState);

            //if (!playerInput.IsMovingHorizontally)
            //    locomotionFsm.ChangeCurrentState((int)PlayerGroundStates.IDLE);
            //else
            //    locomotionFsm.ChangeCurrentState((int)PlayerGroundStates.MOVING);
        }
    }

    private void JumpTransition(InputAction.CallbackContext context)
    {
        if (graceTimer > 0)
        {
            playerEntity.velocity.y = playerMove.jumpVelocity;
            playerEntity.baseMovementFSM.ChangeCurrentState(playerEntity.JumpState);
        }
    }

    private void ResetGraceTime()
    {
        graceTimer = playerMove.graceTime;
        Debug.Log("GG");
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
