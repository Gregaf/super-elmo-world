using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SMTest
{
    public class Big : State
    {
        private PlayerController playerController;
        private AnimatorOverrideController bigAnimations;
        private PlayerGrowth playerGrowth;
        private Vector3 playerScale;

        public Big(PlayerController playerController, AnimatorOverrideController bigAnimations, PlayerGrowth playerGrowth)
        {
            this.playerController = playerController;
            this.bigAnimations = bigAnimations;
            this.playerGrowth = playerGrowth;

            playerScale = new Vector2(0.85f, 1.75f);
        }

        public override void Enter()
        {
            playerController.animator.runtimeAnimatorController = bigAnimations;

            playerController.Controller2D.ModifySize(playerScale);

            playerGrowth.OnShrink += ShrinkTo;
        }

        public override void Exit()
        {
            playerGrowth.OnShrink -= ShrinkTo;

        }

        public override void Tick()
        {

        }

        private void ShrinkTo()
        {
            // Change to Dead state.
        }
    }
}