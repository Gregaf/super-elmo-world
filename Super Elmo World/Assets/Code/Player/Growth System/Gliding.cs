using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BaseGame;

public class Gliding : IState
{
    private PlayerController playerBrain;
    private CharacterController2D controller2D;

    private Vector2 playerScale;
    private Animator animator;

    public Gliding(PlayerController playerBrain, Animator animator)
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

    public void OnTriggerEnter(Collider2D collider2D)
    {
        throw new System.NotImplementedException();
    }

    public void Tick()
    {
        
    }
}
