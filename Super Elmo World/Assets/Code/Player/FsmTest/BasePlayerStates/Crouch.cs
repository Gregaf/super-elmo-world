using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace SMTest
{
    public class Crouch : PlayerState
    {

        private Vector2 crouchSize;
        private Vector2 originalSize;

        public Crouch(PlayerController playerEntity, FSM locomotionFsm) : base(playerEntity, locomotionFsm)
        {
            crouchSize = new Vector2(1, 0.9f);
        }

        public override void Enter()
        {
            originalSize = playerEntity.entityCollider.size;

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
            float crouchValue = controller2D.ControlState.isGrounded ? 0 : 6;

            

            controller2D.SetHorizontalForce(Mathf.Lerp(controller2D.Velocity.x, crouchValue * playerInput.MovementInput.x, Time.deltaTime * playerMove.groundAcceleration));
            controller2D.SetVerticalForce(Mathf.Lerp(controller2D.Velocity.y, -playerMove.baseGravity, Time.deltaTime));

            if (CanStand() && playerInput.MovementInput.y != -1)
            {
                locomotionFsm.ReturnToPreviousState();
            }

        }

        private void CrouchJump(InputAction.CallbackContext context)
        {
            if(controller2D.ControlState.isGrounded)
                controller2D.SetVerticalForce(playerMove.jumpVelocity / 1.25f);
        }

        public bool CanStand()
        {
            float yPoint = Mathf.Abs(crouchSize.y - originalSize.y);
            Vector2 standPoint = new Vector2(playerEntity.entityCollider.bounds.center.x, playerEntity.entityCollider.bounds.max.y + yPoint);

            Debug.DrawLine(playerEntity.transform.position, standPoint);

            return !Physics2D.OverlapPoint(standPoint, controller2D.platformMask);
        }
    }
}