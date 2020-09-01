using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SMTest
{
    public class GroundMovement : MovementState
    {
        private PlayerController playerController;

        public enum PlayerGroundStates
        { 
            IDLE,
            MOVING,
            FALLING,
            JUMPING,
            WALLSLIDING
        }


        public GroundMovement(PlayerController playerController)
        {
            this.playerController = playerController;

            locoMotionFsm.AddToStateList((int) PlayerGroundStates.IDLE, new Idle(playerController, locoMotionFsm));
            locoMotionFsm.AddToStateList((int) PlayerGroundStates.MOVING, new Move(playerController, locoMotionFsm));
            locoMotionFsm.AddToStateList((int) PlayerGroundStates.FALLING, new Fall(playerController, locoMotionFsm));
            locoMotionFsm.AddToStateList((int) PlayerGroundStates.JUMPING, new Jump(playerController, locoMotionFsm));
            locoMotionFsm.AddToStateList((int) PlayerGroundStates.WALLSLIDING, new WallSliding(playerController, locoMotionFsm));

        }

        public override void Enter()
        {
            locoMotionFsm.InitializeFSM((int) PlayerGroundStates.IDLE);

            // Enable the controls for ground...
        }

        public override void Exit()
        {
            // Disable ground controls...
        }

        public override void Tick()
        {
            locoMotionFsm.UpdateCurrentState();
        }
        
    }
}