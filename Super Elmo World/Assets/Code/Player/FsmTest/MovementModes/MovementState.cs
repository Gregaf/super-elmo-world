using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SMTest
{
    public class MovementState : State
    {
        protected FSM locoMotionFsm;

        public MovementState()
        {
            locoMotionFsm = new FSM();
        }

        public override void Enter()
        {
        }

        public override void Exit()
        {
        }

        public override void Tick()
        {
        }
    }
}