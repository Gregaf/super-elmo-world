using SMTest;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Small : State
{
    private SMTest.PlayerController playerController;
    private AnimatorOverrideController smallAnimations;
    private PlayerGrowth playerGrowth;
    private Vector3 playerScale;

    public Small(SMTest.PlayerController playerController, AnimatorOverrideController smallAnimations, PlayerGrowth playerGrowth)
    {
        this.playerController = playerController;
        this.smallAnimations = smallAnimations;
        this.playerGrowth = playerGrowth;

        playerScale = new Vector2(0.85f, 1.25f);
    }

    public override void Enter()
    {
        playerController.animator.runtimeAnimatorController = smallAnimations;

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
