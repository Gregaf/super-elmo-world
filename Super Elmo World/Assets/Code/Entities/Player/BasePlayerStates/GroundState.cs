using SMTest;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;


public class GroundState : PlayerState
{
    private float successfulJumpTime = 0.085f;
    private float successfulJumpTimer = 0;

    private int requiredSuccessfulJumps = 2;
    private int successCount = 0;

    public GroundState(PlayerController playerEntity) : base(playerEntity)
    {

    }

    public override void Enter()
    {
        playerInput.playerControls.Basic.Jump.performed += TransitionToJump;

        playerMove.dynamicGravity = playerMove.baseGravity;

        successfulJumpTimer = successfulJumpTime;
    }

    public override void Exit()
    {
        playerInput.playerControls.Basic.Jump.performed -= TransitionToJump;

    }

    public override void Tick()
    {
        if (successfulJumpTimer <= 0)
            successCount = 0;

        successfulJumpTimer -= Time.deltaTime;

        playerEntity.velocity.y += -(playerMove.dynamicGravity * Time.deltaTime);


        if (playerInput.MovementInput.y == -1)
        {
            playerEntity.baseMovementFSM.ChangeCurrentState(playerEntity.CrouchState);

        }

        if (!playerEntity.controller2D.collisionInfo.isGrounded)
        {
            // Reset y velocity as it accumulates.
            //playerEntity.entityVelocity.y = 0;
            playerEntity.baseMovementFSM.ChangeCurrentState(playerEntity.FallState);
        }
    }

    private void TransitionToJump(InputAction.CallbackContext context)
    {
        if (successfulJumpTimer > 0)
            successCount++;

        if (successCount == requiredSuccessfulJumps)
        {
            successCount = 0;
            playerEntity.velocity.y = playerMove.jumpVelocity * 1.5f;

        }
        else
        {
            playerEntity.velocity.y = playerMove.jumpVelocity;
        }

        playerEntity.baseMovementFSM.ChangeCurrentState(playerEntity.JumpState);
    }
}

