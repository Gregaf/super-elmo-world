﻿using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInputHandler : MonoBehaviour
{
    public Vector2 MovementInput { get; private set; }
    public bool JumpButtonActive { get; private set; }
    public bool RunButtonActive { get; private set; }
    public PlayerControls playerControls { get; private set; }

    private PlayerMovement movement;


    private void Awake()
    {
        playerControls = new PlayerControls();
        movement = this.GetComponent<PlayerMovement>();

        playerControls.Basic.Jump.started += Jumping;
        playerControls.Basic.Jump.canceled += Jumping;

        playerControls.Basic.Run.started += Running;
        playerControls.Basic.Run.canceled += Running;

    }

    private void OnEnable()
    {
        playerControls.Enable();
    }
    private void OnDisable()
    {
        playerControls.Disable();

        playerControls.Basic.Jump.started -= Jumping;
        playerControls.Basic.Jump.canceled -= Jumping;

        playerControls.Basic.Run.started -= Running;
        playerControls.Basic.Run.canceled -= Running;
    }

    private void Running(InputAction.CallbackContext context)
    {
        if (context.started)
            RunButtonActive = true;
        else if (context.canceled)
            RunButtonActive = false;
    }

    private void Jumping(InputAction.CallbackContext context)
    {
        if (context.started)
            JumpButtonActive = true;
        else if (context.canceled)
            JumpButtonActive = false;
    }

    private void Update()
    {
        MovementInput = playerControls.Basic.Move.ReadValue<Vector2>();
        

    }

}