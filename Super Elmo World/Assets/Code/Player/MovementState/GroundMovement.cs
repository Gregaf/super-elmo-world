using UnityEditor.Animations;
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
    private float jumpVelocity;

    private bool isCrouching = false;
    private Vector2 crouchScale;
    private Animator animator;
    public GroundMovement(FSM ownerFsm, PlayerInputHandler playerInput, CharacterController2D controller2D, GroundMoveProperties groundMoveProperties, Animator animator)
    {
        this.ownerFsm = ownerFsm;
        this.playerInput = playerInput;
        this.controller2D = controller2D;
        this.groundMoveProperties = groundMoveProperties;
        this.animator = animator;

        crouchScale = new Vector2(1, 0.9f);
    }

    public void Enter()
    {
        playerInput.playerControls.Basic.Jump.performed += OnJump;

        groundMoveProperties.gravity = (groundMoveProperties.jumpHeight) / (2 * (groundMoveProperties.jumpTime * groundMoveProperties.jumpTime));
        dynamicGravity = groundMoveProperties.gravity;

        jumpVelocity = Mathf.Sqrt(2 * groundMoveProperties.jumpHeight * dynamicGravity);
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
            controller2D.SetVerticalForce(jumpVelocity);

            AudioManager.Instance.PlaySingleRandomSfx(groundMoveProperties.jumpSfx);
        }
    }

    // This method handles staying in air longer while jump button is held, then falling faster on release.
    private void HeldJump()
    {
        if (!playerInput.JumpButtonActive || controller2D.Velocity.y < 0)
        {
            if (!controller2D.ControlState.isGrounded)
            {
                dynamicGravity = groundMoveProperties.gravity * groundMoveProperties.fallMultiplier;
            }
            else
                dynamicGravity = groundMoveProperties.gravity;
        }

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
        string format = $"Current State: 'Ground', Current Speed: {currentSpeed} Current Gravity: {dynamicGravity}";

        return format;
    }
}
