using SMTest;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SMTest
{
    public class PlayerState : State
    {
        protected PlayerController playerEntity;
        protected PlayerInputHandler playerInput;
        protected CharacterController2D controller2D;
        protected PlayerMoveProperties playerMove;
        protected FSM locomotionFsm;

        public PlayerState(PlayerController playerEntity, FSM locomotionFsm)
        {
            this.playerEntity = playerEntity;
            this.locomotionFsm = locomotionFsm;

            playerInput = playerEntity.PlayerInputHandler;
            controller2D = playerEntity.Controller2D;
            playerMove = playerEntity.playerMoveProperties;
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