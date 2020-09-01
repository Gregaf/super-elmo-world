using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace SMTest
{
    public class WallSliding : PlayerState
    {

        private int wallDirection = 0;
        private float timeTillUnstick = 0.12f;
        private float wallStickTimer;
        private bool unstickTimerStart;

        public WallSliding(PlayerController playerEntity, FSM locomotionFsm) : base(playerEntity, locomotionFsm)
        {

        }

        public override void Enter()
        {
            wallDirection = controller2D.ControlState.isCollidingLeft ? -1 : 1;

            wallStickTimer = timeTillUnstick;

            unstickTimerStart = false;

            controller2D.SetVerticalForce(1);

            playerEntity.animator.SetBool("WallSliding", true);

            playerInput.playerControls.Basic.Jump.performed += LaunchFromWall;    
        }

        public override void Exit()
        {
            playerInput.playerControls.Basic.Jump.performed -= LaunchFromWall;

            playerEntity.animator.SetBool("WallSliding", false);
        }

        public override void Tick()
        {

            if (playerInput.MovementInput.x != wallDirection)
                unstickTimerStart = true;

            if (unstickTimerStart)
                wallStickTimer -= Time.deltaTime;


            if (wallStickTimer <= 0 || !playerEntity.TouchingWall(wallDirection))
                locomotionFsm.ChangeCurrentState((int) PlayerGroundStates.FALLING);
            else
                controller2D.SetHorizontalForce(0);
                    

            if (controller2D.ControlState.isGrounded)
            {
                locomotionFsm.ChangeCurrentState((int) PlayerGroundStates.IDLE);
            }

            controller2D.SetVerticalForce(Mathf.Lerp(controller2D.Velocity.y, -playerMove.wallSlideSpeed, Time.deltaTime * 3f));
        }

        private void LaunchFromWall(InputAction.CallbackContext context)
        {                
            controller2D.AddForce(new Vector2(-wallDirection * playerMove.wallLaunchVelocity.x, playerMove.wallLaunchVelocity.y));             
            wallStickTimer = 0;
                
            //locomotionFsm.ChangeCurrentState("Fall");        
        }
    }
}