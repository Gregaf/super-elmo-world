using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class BNormal : IState
{
    private FSM fsm;
    private CharacterController2D controller2D;
    private Enemy owner;
    private float moveSpeed;
    private float gravity;

    private Transform ownerTransform;
    private int moveDirection = 0;


    public BNormal(FSM fsm, CharacterController2D controller2D, Enemy owner, float moveSpeed, float gravity)
    {
        this.fsm = fsm;
        this.controller2D = controller2D;
        this.owner = owner;
        this.moveSpeed = moveSpeed;
        this.gravity = gravity;

        moveDirection = owner.isFacingRight ? 1 : -1;
        ownerTransform = controller2D.transform;
    }

    public void Enter()
    {

    }

    public void Exit()
    {
    }

    public void Tick()
    {
        controller2D.SetHorizontalForce(Mathf.Lerp(controller2D.Velocity.x, moveSpeed * moveDirection, Time.deltaTime * 8));

        controller2D.SetVerticalForce(Mathf.Lerp(controller2D.Velocity.y, -gravity, Time.deltaTime));

        if ((controller2D.ControlState.isCollidingRight && owner.isFacingRight) || (controller2D.ControlState.isCollidingLeft && !owner.isFacingRight))
        {
            owner.Flip();
            moveDirection = owner.isFacingRight ? 1 : -1;
        }
    }

    public void OnTriggerEnter(Collider2D collider2D)
    {
        if (collider2D.GetComponent<PlayerController>() != null)
        {
            PlayerController playerTouched = collider2D.GetComponent<PlayerController>();

            if (playerTouched.IsMovingDown() && playerTouched.transform.position.y > ownerTransform.position.y)
            {
                playerTouched.Bounce(15f);
                fsm.ChangeCurrentState("Dead");
            }
        }
    }
}
