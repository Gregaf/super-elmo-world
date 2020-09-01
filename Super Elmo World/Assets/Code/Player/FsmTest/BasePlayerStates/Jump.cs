using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SMTest
{
    public class Jump : AirState
    {
        public Jump(PlayerController playerEntity, FSM locomotionFsm) : base(playerEntity, locomotionFsm)
        {
        }

        public override void Enter()
        {
            base.Enter();
            SetJump();
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

            if (controller2D.Velocity.y < -0.1f)
            {
                locomotionFsm.ChangeCurrentState((int) PlayerGroundStates.FALLING);
            }
        }

        private void SetJump()
        {
            controller2D.SetVerticalForce(playerMove.jumpVelocity);

        }
    }
}
