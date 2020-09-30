using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Big : State
{
    private PlayerController playerController;
    private AnimatorOverrideController bigAnimations;

    private Vector3 playerScale;

    public Big(PlayerController playerController, AnimatorOverrideController bigAnimations)
    {
        this.playerController = playerController;
        this.bigAnimations = bigAnimations;

        playerScale = new Vector2(0.85f, 1.75f);
    }

    public override void Enter()
    {
        playerController.animator.runtimeAnimatorController = bigAnimations;

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
        playerController.growthFsm.ChangeCurrentState((int)PlayerGrowthStates.SMALL);
    }
}
