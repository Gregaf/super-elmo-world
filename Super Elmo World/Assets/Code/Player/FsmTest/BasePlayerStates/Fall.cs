using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SMTest
{
    public class Fall : AirState
    {
        public Fall(PlayerController playerEntity, FSM locomotionFsm) : base(playerEntity, locomotionFsm)
        {
        }

        public override void Enter()
        {
            base.Enter();

            playerMove.dynamicGravity = playerMove.baseGravity * playerMove.fallMultiplier;
        }

        public override void Exit()
        {
            base.Exit();

        }

        public override void Tick()
        {
            base.Tick();
            

            if (controller2D.ControlState.isGrounded)
            {
                if (!playerInput.IsMovingHorizontally)
                    locomotionFsm.ChangeCurrentState((int) PlayerGroundStates.IDLE);
                else
                    locomotionFsm.ChangeCurrentState((int) PlayerGroundStates.MOVING);
            }
        }
    }
}