using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Move : GroundState
{

    float storeFloat;
    public Move(PlayerController playerEntity) : base(playerEntity)
    {
    }

    public override void Enter()
    {
        base.Enter();

    }

    public override void Exit()
    {
        base.Exit();

    }

    public override void Tick()
    {
        base.Tick();

        playerMove.currentSpeed = HandleRunSpeed(playerMove.currentSpeed, playerMove.runAcceleration);

        //controller2D.SetHorizontalForce(Mathf.Lerp(controller2D.Velocity.x, playerMove.currentSpeed * playerInput.MovementInput.x, Time.deltaTime * playerMove.groundAcceleration));
        playerEntity.velocity.x = Mathf.SmoothDamp(playerEntity.velocity.x, playerMove.currentSpeed * playerInput.MovementInput.x, ref storeFloat, playerMove.groundAccelerationTime);

        if (!playerInput.IsMovingHorizontally)
            playerEntity.baseMovementFSM.ChangeCurrentState(playerEntity.IdleState);
    }

    // Takes a float for the speed to be altered, also takes a float for how fast speed is increased and decreased.
    // Returns the newSpeed that was adjusted and clamped.
    private float HandleRunSpeed(float newSpeed, float runAccelerationRate)
    {
        if (playerInput.RunButtonActive)
            newSpeed += Time.deltaTime * runAccelerationRate;
        else
            newSpeed -= Time.deltaTime * runAccelerationRate;

        newSpeed = Mathf.Clamp(newSpeed, playerMove.walkSpeed, playerMove.runSpeed);

        return newSpeed;
    }
}
