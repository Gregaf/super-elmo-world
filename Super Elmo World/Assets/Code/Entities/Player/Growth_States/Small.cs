using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Small : GrowthState
{
    private AnimatorOverrideController smallAnimations;

    private Vector3 playerScale;

    public Small(PlayerController playerController, float shieldAmount, AnimatorOverrideController smallAnimations) : base(playerController, shieldAmount)
    {
        this.smallAnimations = smallAnimations;

    }

    public override void Enter()
    {
        base.Enter();
        playerController.animator.runtimeAnimatorController = smallAnimations;

        playerController.Control2D.ModifySize(new Vector2(0.75f, 1f));
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
        
    }

    protected override void ShrinkTo()
    {
        playerController.growthFsm.ChangeCurrentState((int) PlayerGrowthStates.DEAD);
    }
}
