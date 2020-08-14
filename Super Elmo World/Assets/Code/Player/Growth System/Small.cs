using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Small : IState
{
    private PlayerBrain playerBrain;
    private Vector3 playerScale;
    private Animator animator;

    public Small(PlayerBrain playerBrain, Animator animator)
    {
        this.playerBrain = playerBrain;
        this.animator = animator;

        playerScale = new Vector2(0.85f, 1.25f);

    }

    public void Enter()
    {
        animator.runtimeAnimatorController = playerBrain.smallAnimations;
        // Switch to small sprite...
        playerBrain.ModifySize(playerScale);
    }

    public void Exit()
    {
        
    }

    public void StateUpdate()
    {
        // May not need anything as of now since the small state has no extra functionality.
    }
}
