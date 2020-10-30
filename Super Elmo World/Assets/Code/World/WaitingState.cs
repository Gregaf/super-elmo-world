using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class WaitingState : State
{
    Character owner;
    PlayerInputHandler playerInput;

    public WaitingState()
    {

    }

    public override void Enter()
    {
        playerInput.playerControls.World.Select.performed += SelectLevel;

        owner.CheckNode();
    }

    public override void Exit()
    {
        playerInput.playerControls.World.Select.performed -= SelectLevel;


    }

    public override void OnTriggerEnter2D(Collider2D collider2D)
    {
    }

    public override void Tick()
    {
    }

    private void SelectLevel(InputAction.CallbackContext context)
    { 
        
    }

}
