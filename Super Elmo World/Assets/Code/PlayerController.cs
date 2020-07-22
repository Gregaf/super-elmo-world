using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEditor.Tilemaps;
using UnityEngine;

public enum MovementMode
{
    Ground,
    Flying,
    Swimming
}
public class PlayerController : MonoBehaviour
{
    public MovementMode mode;

    private CharacterController2D physicsController;
    private PlayerInput playerInput;

    public float maxRunSpeed;
    public float walkSpeed;
    private float currentSpeed;

    public float jumpVelocity;
    public float fallMultiplier;
    public float lowJumpMultiplier;

    float acceleration { get { return (physicsController.ControlState.isGrounded ? groundAcceleration : airAcceleration); } }
    [SerializeField]
    private float groundAcceleration = 6;
    [SerializeField]
    private float airAcceleration = 3;

    private void OnEnable()
    {
        Block.onHitBlock += PushDown;
    }

    private void OnDisable()
    {
        Block.onHitBlock -= PushDown;

    }

    private void Awake()
    {
        physicsController = this.GetComponent<CharacterController2D>();
        playerInput = this.GetComponent<PlayerInput>();
    }

    private void Start()
    {

    }

    private void Update()
    {
        xMove();

        yMove();

        Jump();


        physicsController.Parameters.gravity = -(1 / currentSpeed) * 200;
        
    }


    private void xMove()
    {
    
        if (mode == MovementMode.Ground)
        {
            // Running.
            if (playerInput.RunButtonHeld && Mathf.Abs(playerInput.MovementInput.x) > 0)
            {
                currentSpeed = Mathf.Clamp(currentSpeed, 0, maxRunSpeed);

                currentSpeed += (Time.deltaTime * 5);
            }
            else
            {
                currentSpeed = Mathf.Clamp(currentSpeed, walkSpeed, maxRunSpeed);

                currentSpeed -= (Time.deltaTime * 5);
            }

            physicsController.SetHorizontalForce(Mathf.Lerp(physicsController.Velocity.x, currentSpeed * playerInput.MovementInput.x, Time.deltaTime * acceleration));
        }
        // Apply constant movement to the right
        else if (mode == MovementMode.Flying)
            physicsController.SetHorizontalForce(Mathf.Lerp(physicsController.Velocity.x, physicsController.Parameters.gravity, Time.deltaTime));
        else if (mode == MovementMode.Swimming)
            physicsController.SetHorizontalForce(Mathf.Lerp(physicsController.Velocity.x, currentSpeed * playerInput.MovementInput.x, Time.deltaTime));

    }

    private void yMove()
    {
        if(mode == MovementMode.Ground)
            physicsController.SetVerticalForce(Mathf.Lerp(physicsController.Velocity.y, physicsController.Parameters.gravity, Time.deltaTime));
        // Apply up and down movement based on Player Input
        else if(mode == MovementMode.Flying)
            physicsController.SetVerticalForce(Mathf.Lerp(physicsController.Velocity.y, currentSpeed * playerInput.MovementInput.y, Time.deltaTime));
        else if(mode == MovementMode.Swimming)
            physicsController.SetVerticalForce(Mathf.Lerp(physicsController.Velocity.y, physicsController.Parameters.gravity, Time.deltaTime));

    }

    private void Jump()
    {
        if (playerInput.JumpButtonPressed && physicsController.ControlState.isGrounded)
        {
            physicsController.SetVerticalForce(jumpVelocity);
        }
        
        if (physicsController.Velocity.y < 0)
            physicsController.AddForce(Vector2.up * -(fallMultiplier));
        else if (!playerInput.JumpButtonHeld && physicsController.Velocity.y > 0)
            physicsController.AddForce(Vector2.up * -(lowJumpMultiplier));
       

    }

    private void PushDown()
    {
        physicsController.SetForce(new Vector2(physicsController.Velocity.x, -physicsController.Velocity.y));

    }

}
