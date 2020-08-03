using System;
using UnityEngine;
using UnityEngine.InputSystem;

[Serializable]
public class GroundMoveProperties
{
    public float baseSpeed;
    public float runSpeed;
    public float runAcceleration;
    public float airAcceleration;
    public float groundAcceleration;
    public float jumpVelocity;
    public float gravity;
}

[Serializable]
public class FlyingMoveProperties
{
    public float moveSpeed;
    public float levelSpeed;
}

public class PlayerMovement : MonoBehaviour
{
    private PlayerInputHandler playerInput;
    private CharacterController2D controller2D;
    private FSM movementState;
    [SerializeField] private GroundMoveProperties groundMoveProperties = null;
    [SerializeField] private FlyingMoveProperties flyingMoveProperties = null;
    
    private void Awake()
    {
        controller2D = this.GetComponent<CharacterController2D>();
        playerInput = this.GetComponent<PlayerInputHandler>();

        movementState = new FSM();
        movementState.AddToStateList("Ground", new GroundMovement(movementState, playerInput, controller2D, groundMoveProperties));
        movementState.AddToStateList("Flying", new FlyingMovement());

    }

    private void Start()
    {
        movementState.InitializeFSM(movementState.GetState("Ground"));

    }

    private void Update()
    {
        movementState.UpdateCurrentState();

        if (Input.GetKeyDown(KeyCode.U))
            movementState.ChangeCurrentState("Flying");

        if (Input.GetKeyDown(KeyCode.T))
            movementState.ChangeCurrentState("Ground");

    }


}
