using SMTest;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SMTest
{
    public class Move : GroundState
    {
        public Move(PlayerController playerEntity, FSM locomotionFsm) : base(playerEntity, locomotionFsm)
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

            playerMove.currentSpeed = HandleRunSpeed(playerMove.currentSpeed, playerMove.runAcceleration);

            controller2D.SetHorizontalForce(Mathf.Lerp(controller2D.Velocity.x, playerMove.currentSpeed * playerInput.MovementInput.x, Time.deltaTime * playerMove.groundAcceleration));

            if (!playerInput.IsMovingHorizontally)
                locomotionFsm.ChangeCurrentState((int) PlayerGroundStates.IDLE);
        }

        // Takes a float for the speed to be altered, also takes a float for how fast speed is increased and decreased.
        // Returns the newSpeed that was adjusted and clamped.
        private float HandleRunSpeed(float newSpeed, float runAccelerationRate)
        {
            if (playerInput.RunButtonActive)
                newSpeed += Time.deltaTime * runAccelerationRate;
            else
                newSpeed -= Time.deltaTime * runAccelerationRate;

            newSpeed = Mathf.Clamp(newSpeed, playerMove.baseSpeed, playerMove.runSpeed);

            return newSpeed;
        }
    }
}