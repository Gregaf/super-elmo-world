using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundPound : PlayerState
{
    private float pauseTime = .25f;
    private float pauseTimer = 0;

    private float landTime = .125f;
    private float landTimer = 0;

    public GroundPound(PlayerController playerEntity) : base(playerEntity)
    {

    }

    public override void Enter()
    {
        pauseTimer = pauseTime;

        landTimer = landTime;

        //controller2D.SetForce(Vector2.zero);

        playerEntity.animator.SetBool("GroundPound", true);
    }

    public override void Exit()
    {
        pauseTimer = pauseTime;

        playerEntity.animator.SetBool("GroundPound", false);
    }

    public override void OnTriggerEnter2D(Collider2D collider2D)
    {
    }

    public override void Tick()
    {
        if (pauseTimer < 0)
        {
            //controller2D.SetVerticalForce(Mathf.Lerp(controller2D.Velocity.y, -40f, Time.deltaTime * 5));

        }

        if (controller2D.collisionInfo.isGrounded)
        {
            landTimer -= Time.deltaTime;

            if (landTimer <= 0)
                playerEntity.baseMovementFSM.ChangeCurrentState(playerEntity.IdleState);

        }

        pauseTimer -= Time.deltaTime;
    }
}
