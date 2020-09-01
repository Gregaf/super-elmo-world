using SMTest;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SMTest
{

    public class Idle : GroundState
    {
        public Idle(PlayerController playerEntity, FSM locomotionFsm) : base(playerEntity, locomotionFsm)
        {
        }

        public override void Enter()
        {
            base.Enter();
        }

        public override void Exit()
        {
            base.Exit();

        }

        public override void Tick()
        {
            base.Tick();

            Debug.Log(controller2D.ControlState.isCollidingLeft || controller2D.ControlState.isCollidingRight);

            // Need to multiply by Acceleration.
            controller2D.SetHorizontalForce(Mathf.Lerp(controller2D.Velocity.x, 0, Time.deltaTime * playerMove.groundAcceleration));

            if (playerInput.IsMovingHorizontally)
            {
                locomotionFsm.ChangeCurrentState((int) PlayerGroundStates.MOVING);
            }
        }
    }
}
