using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Big : IState
{
    private PlayerBrain playerBrain;
    private Animator animator;
    private Vector3 playerScale;

    public Big(PlayerBrain playerBrain, Animator animator)
    {
        this.playerBrain = playerBrain;
        this.animator = animator;

        playerScale = new Vector2(0.85f, 1.75f);
    }

    public void Enter()
    {
        animator.runtimeAnimatorController = playerBrain.bigAnimations;
        playerBrain.ModifySize(playerScale);
        // Maybe playerBrain.EnableStrongerHead(); so that way bigger blocks can be broken.
    }

    public void Exit()
    {
    }

    public void StateUpdate()
    {
    }
}
