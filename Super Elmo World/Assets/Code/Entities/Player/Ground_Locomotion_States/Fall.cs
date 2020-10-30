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

        controller2D.OnTriggerEnter += Stomp;

        playerInput.playerControls.Basic.Jump.performed += JumpTransition;

        controller2D.OnFellEvent += ResetOnFall;
    }

    public override void Exit()
    {
        base.Exit();

        controller2D.OnTriggerEnter -= Stomp;

        playerInput.playerControls.Basic.Jump.performed -= JumpTransition;

        controller2D.OnFellEvent -= ResetOnFall;
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

    private void ResetOnFall()
    {
        graceTimer = playerMove.graceTime;
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

    private void Stomp(Collider2D otherCollider)
    {
        if (otherCollider.GetComponent<IDamageable>() != null)
        {

            otherCollider.GetComponent<IDamageable>().TakeDamage(1);

        }
    }
}
