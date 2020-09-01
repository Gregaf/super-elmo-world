using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SMTest
{
    public class AirState : PlayerState
    {
        private float wallJumpTimer;
        private float wallJumpWaitTime = 0.05f;

        public AirState(PlayerController playerEntity, FSM locomotionFsm) : base(playerEntity, locomotionFsm)
        {

        }

        public override void Enter()
        {
            if (locomotionFsm.PreviousState == locomotionFsm.GetState((int) PlayerGroundStates.WALLSLIDING))
            {
                wallJumpTimer = wallJumpWaitTime;
            }

        }

        public override void Exit()
        {

        }

        public override void Tick()
        {
            wallJumpTimer -= Time.deltaTime;

            controller2D.SetHorizontalForce(Mathf.Lerp(controller2D.Velocity.x, playerInput.MovementInput.x * playerMove.currentSpeed, Time.deltaTime * playerMove.airAcceleration));
            controller2D.SetVerticalForce(Mathf.Lerp(controller2D.Velocity.y, -playerMove.dynamicGravity, Time.deltaTime));

            if (playerEntity.TouchingWall((int) playerInput.MovementInput.x) && wallJumpTimer < 0)
                locomotionFsm.ChangeCurrentState((int) PlayerGroundStates.WALLSLIDING);
        }

        
        
    }
}