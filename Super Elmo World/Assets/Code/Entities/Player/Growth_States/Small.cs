using SMTest;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Small : State
{
    private PlayerController playerController;
    private AnimatorOverrideController smallAnimations;

    private Vector3 playerScale;

    public Small(PlayerController playerController, AnimatorOverrideController smallAnimations)
    {
        this.playerController = playerController;
        this.smallAnimations = smallAnimations;

        playerScale = new Vector2(0.85f, 1.25f);
    }

    public override void Enter()
    {
        playerController.animator.runtimeAnimatorController = smallAnimations;

        //playerController.Controller2D.ModifySize(playerScale);

        playerController.OnShrink += ShrinkTo;
    }

    public override void Exit()
    {
        playerController.OnShrink -= ShrinkTo;
    }

    public override void OnTriggerEnter2D(Collider2D collider2D)
    {
    }

    public override void Tick()
    {
        
    }

    private void ShrinkTo()
    { 
        // Change to Dead state.
    }
}
