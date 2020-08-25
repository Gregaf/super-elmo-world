using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Small : IState
{
    private PlayerController playerBrain;
    private Vector3 playerScale;
    private Animator animator;

    public Small(PlayerController playerBrain, Animator animator)
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

    public void OnTriggerEnter(Collider2D collider2D)
    {

    }

    public void Tick()
    {
        // May not need anything as of now since the small state has no extra functionality.
    }
}
