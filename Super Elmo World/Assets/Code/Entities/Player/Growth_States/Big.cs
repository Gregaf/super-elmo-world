using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Big : GrowthState
{
    private AnimatorOverrideController bigAnimations;


    public Big(PlayerController playerController, float shieldAmount, AnimatorOverrideController bigAnimations) : base(playerController, shieldAmount)
    {
        this.bigAnimations = bigAnimations;

    }

    public override void Enter()
    {
        base.Enter();
        playerController.animator.runtimeAnimatorController = bigAnimations;

        playerController.Control2D.ModifySize(new Vector2(0.75f, 1.4f));

    }

    public override void Exit()
    {
        base.Exit();

    }

    public override void OnTriggerEnter2D(Collider2D collider2D)
    {
    }

    public override void Tick()
    {
        // When Big, can break blocks that are unbreakable by small.

    }

    protected override void ShrinkTo()
    {
        playerController.growthFsm.ChangeCurrentState((int) PlayerGrowthStates.SMALL);
    }
}
