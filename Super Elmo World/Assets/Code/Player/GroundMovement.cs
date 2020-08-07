using UnityEngine;
using UnityEngine.InputSystem;

public class GroundMovement : IState
{
    private FSM ownerFsm;
    private PlayerInputHandler playerInput;
    private CharacterController2D controller2D;
    private GroundMoveProperties groundMoveProperties;

    private float currentSpeed;
    private float dynamicGravity;

    public GroundMovement(FSM ownerFsm, PlayerInputHandler playerInput, CharacterController2D controller2D, GroundMoveProperties groundMoveProperties)
    {
        this.ownerFsm = ownerFsm;
        this.playerInput = playerInput;
        this.controller2D = controller2D;
        this.groundMoveProperties = groundMoveProperties;
    }

    public void Enter()
    {
        playerInput.playerControls.Basic.Jump.performed += OnJump;
    }

    public void Exit()
    {
        playerInput.playerControls.Basic.Jump.performed -= OnJump;

    }

    public void StateUpdate()
    {
        float acceleration = controller2D.ControlState.isGrounded ? groundMoveProperties.groundAcceleration : groundMoveProperties.airAcceleration;

        currentSpeed = HandleRunSpeed(currentSpeed, groundMoveProperties.runAcceleration);

        HeldJump();

        controller2D.SetHorizontalForce(Mathf.Lerp(controller2D.Velocity.x, currentSpeed * playerInput.MovementInput.x, Time.deltaTime * acceleration));

        controller2D.SetVerticalForce(Mathf.Lerp(controller2D.Velocity.y, -dynamicGravity, Time.deltaTime));
    }

    // The parameter context is to access information about the buttonPress associated with the 'performed' event.
    // Simply applys vertical force upward as long as the player is on the ground.
    private void OnJump(InputAction.CallbackContext context)
    {
        if (controller2D.ControlState.isGrounded)
        {
            controller2D.SetVerticalForce(groundMoveProperties.jumpVelocity);
        }
    }

    // Review: I need to create a variable to modify for how fast gravity builds up when not holding button.
    // This method handles staying in air longer while jump button is held, then falling faster on release.
    private void HeldJump()
    {
        if (playerInput.JumpButtonActive && controller2D.Velocity.y > 0)
        {
            dynamicGravity = AlterGravity(currentSpeed);

        }
        else
        {
            if (!controller2D.ControlState.isGrounded)
            {
                dynamicGravity += 1.75f;
            }
        }
    }

    // Takes a float for the currentSpeed the player is moving at.
    // Returns a new gravity value based on the inverse of the current speed.
    private float AlterGravity(float currentSpeed)
    {
        if (currentSpeed <= 0)     
            return groundMoveProperties.gravity;
        

        // Review: What should the arbitrary value be?
        float newGravity = (1 / currentSpeed) * 150;

        newGravity = Mathf.Clamp(newGravity, groundMoveProperties.gravity, 200);

        return newGravity;
    }

    
    // Takes a float for the speed to be altered, also takes a float for how fast speed is increased and decreased.
    // Returns the newSpeed that was adjusted and clamped.
    private float HandleRunSpeed(float newSpeed, float runAccelerationRate)
    {
        if (playerInput.RunButtonActive)
            newSpeed += Time.deltaTime * runAccelerationRate;
        else
            newSpeed -= Time.deltaTime * runAccelerationRate;

        newSpeed = Mathf.Clamp(newSpeed, groundMoveProperties.baseSpeed, groundMoveProperties.runSpeed);

        return newSpeed;
    }

    public override string ToString()
    {
        string format = "Current State: 'Ground', Current Speed: " + currentSpeed + "Current Gravity: " + dynamicGravity;

        return format;
    }
}
