using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gliding : IState
{
    private PlayerBrain playerBrain;
    private CharacterController2D controller2D;

    private Vector2 playerScale;
    private Animator animator;

    public Gliding(PlayerBrain playerBrain, Animator animator)
    {
        this.playerBrain = playerBrain;

        playerScale = new Vector2(0.85f, 1.75f);

        this.animator = animator;
    }

    public void Enter()
    {
        animator.runtimeAnimatorController = playerBrain.hammerAnimations;

        playerBrain.ModifySize(playerScale);
        // controller2D.ModifySize(playerScale);
    }

    public void Exit()
    {
    }

    public void StateUpdate()
    {
        
    }
}
