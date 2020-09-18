using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;


public class Climbing : PlayerState
{
    private float climbSpeed = 3f;

    Vector2 climbBoundedArea;

    public Climbing(PlayerController playerEntity) : base(playerEntity)
    {
    }

    public override void Enter()
    {
        playerEntity.velocity = Vector2.zero;

        playerInput.playerControls.Basic.Jump.performed += TransitionToJump;

    }

    public override void Exit()
    {

        playerInput.playerControls.Basic.Jump.performed -= TransitionToJump;
    }

    public override void Tick()
    {

    }

    private void TransitionToJump(InputAction.CallbackContext context)
    {
    }

}
