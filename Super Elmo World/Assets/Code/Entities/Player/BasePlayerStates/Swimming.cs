using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;


public class Swimming : PlayerState
{
    private float swimTimer;

    public Swimming(PlayerController playerEntity) : base(playerEntity)
    {
    }

    public override void Enter()
    {
        base.Enter();

        playerInput.playerControls.Basic.Jump.performed += Paddle;

    }

    public override void Exit()
    {
        base.Exit();

        playerInput.playerControls.Basic.Jump.performed -= Paddle;

        // Setvertical force to surface velocity.
        //controller2D.SetVerticalForce(15);
    }

    public override void Tick()
    {
        base.Tick();

        swimTimer += Time.deltaTime;

        //controller2D.SetHorizontalForce(Mathf.Lerp(controller2D.Velocity.x, playerMove.swimSpeed * playerInput.MovementInput.x, Time.deltaTime * playerMove.swimAcceleration));

        //controller2D.SetVerticalForce(Mathf.Lerp(controller2D.Velocity.y, -playerMove.swimGravity, Time.deltaTime * .5f));

    }

    private bool AbleToPaddle()
    {
        return (swimTimer >= playerMove.swimJumpInterval);
    }

    private void Paddle(InputAction.CallbackContext context)
    {
        if (!AbleToPaddle())
            return;

        //controller2D.SetVerticalForce(playerMove.paddleVelocity);

        swimTimer = 0;
    }
}
