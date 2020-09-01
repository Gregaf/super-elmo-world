using SMTest;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace SMTest
{

    public class GroundState : PlayerState
    {
        public GroundState(PlayerController playerEntity, FSM locomotionFsm) : base(playerEntity, locomotionFsm)
        {
        }

        public override void Enter()
        {
            playerInput.playerControls.Basic.Jump.performed += TransitionToJump;

            playerMove.dynamicGravity = playerMove.baseGravity;
        }

        public override void Exit()
        {
            playerInput.playerControls.Basic.Jump.performed -= TransitionToJump;

        }

        public override void Tick()
        {
            controller2D.SetVerticalForce(Mathf.Lerp(controller2D.Velocity.y, -playerMove.baseGravity, Time.deltaTime));

            if (playerInput.MovementInput.y == -1 && !playerEntity.isCrouching)
            {
                playerEntity.Crouch();
                
            }

            if (!playerEntity.Controller2D.ControlState.isGrounded)
            {
                locomotionFsm.ChangeCurrentState((int) PlayerGroundStates.FALLING);
            }
        }

        private void TransitionToJump(InputAction.CallbackContext context)
        {
            locomotionFsm.ChangeCurrentState((int) PlayerGroundStates.JUMPING);
        }
    }
}
