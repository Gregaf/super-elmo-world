using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyIdle : IState
{
    private FSM ownerFsm;
    private CharacterController2D controller2D;
    private BoxCollider2D collider2D;
    private LayerMask whoCanGrab;
    private float fallSpeed;

    public KeyIdle(FSM ownerFsm, CharacterController2D controller2D, BoxCollider2D collider2D, LayerMask whoCanGrab, float fallSpeed)
    {
        this.ownerFsm = ownerFsm;
        this.controller2D = controller2D;
        this.collider2D = collider2D;
        this.whoCanGrab = whoCanGrab;
        this.fallSpeed = fallSpeed;
    }

    public void Enter()
    {
        controller2D.HandleCollision = true;
    }

    public void Exit()
    {
        controller2D.SetVerticalForce(0);
        controller2D.HandleCollision = false;
    }

    public void OnTriggerEnter(Collider2D collider2D)
    {
        throw new System.NotImplementedException();
    }

    public void Tick()
    {
        // Play idling animation.

        controller2D.SetVerticalForce(Mathf.Lerp(controller2D.Velocity.y, fallSpeed, Time.deltaTime));

        if (Physics2D.OverlapBox(collider2D.gameObject.transform.position, collider2D.size, 0, whoCanGrab))
        {
            ownerFsm.ChangeCurrentState("Follow");
        }

    }

}
