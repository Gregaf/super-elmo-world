using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;


public class Crouch : PlayerState
{

    private Vector2 crouchSize;
    private Vector2 originalSize;
    private float store;

    public Crouch(PlayerController playerEntity) : base(playerEntity)
    {
        crouchSize = new Vector2(1, 0.75f);
    }

    public override void Enter()
    {
        originalSize = playerEntity.EntityCollider.size;

        playerInput.playerControls.Basic.Jump.performed += CrouchJump;

        // Change to crouch size.
        controller2D.ModifySize(crouchSize);

        playerEntity.animator.SetBool("isCrouching", true);
    }

    public override void Exit()
    {
        playerInput.playerControls.Basic.Jump.performed -= CrouchJump;
        // Change back to regular size.
        controller2D.ModifySize(originalSize);

        playerEntity.animator.SetBool("isCrouching", false);
    }

    public override void Tick()
    {
        //controller2D.SetHorizontalForce(Mathf.Lerp(controller2D.Velocity.x, 0, Time.deltaTime * playerMove.groundAcceleration));
        //controller2D.SetVerticalForce(Mathf.Lerp(controller2D.Velocity.y, -playerMove.baseGravity * 4, Time.deltaTime));

        //playerEntity.velocity.x = Mathf.Lerp(playerEntity.velocity.x, 0, Time.deltaTime * playerMove.groundAcceleration);
        //playerEntity.velocity.y = Mathf.Lerp(playerEntity.velocity.y, -playerMove.baseGravity * 4, Time.deltaTime);

        playerEntity.velocity.y += playerMove.normalGravity * 4 * Time.deltaTime;
        playerEntity.velocity.x = Mathf.SmoothDamp(playerEntity.velocity.x, 0, ref store, 0.35f);

        if (CanStand() && playerInput.MovementInput.y != -1)
        {
            playerEntity.baseMovementFSM.ReturnToPreviousState();
        }

    }

    private void CrouchJump(InputAction.CallbackContext context)
    {
        if (controller2D.collisionInfo.isGrounded)
        {
            playerEntity.velocity = new Vector2((2 * playerInput.MovementInput.x) + playerEntity.velocity.x, playerMove.jumpVelocity / 1.25f);
            //controller2D.SetForce(new Vector2(2 * playerInput.MovementInput.x, playerMove.jumpVelocity / 1.25f));
        }
    }

    public bool CanStand()
    {
        float yPoint = controller2D.YAxisIsInverted ? crouchSize.y - originalSize.y : originalSize.y - crouchSize.y;
        float extent = controller2D.YAxisIsInverted ? playerEntity.EntityCollider.bounds.min.y : playerEntity.EntityCollider.bounds.max.y;

        Vector2 standPoint = new Vector2(playerEntity.EntityCollider.bounds.center.x, extent + yPoint);

        
            

        Debug.DrawLine(playerEntity.transform.position, standPoint);
        
        return !Physics2D.OverlapPoint(standPoint, controller2D.CollisionMask);
    }
}
