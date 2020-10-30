using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatrolWander : EnemyState
{

    private int direction = 1;
    private float moveSpeed = 3f;
    private float store;

    public PatrolWander(Enemy owner, Controller2D controller2D, HealthHandler healthHandler) : base(owner, controller2D, healthHandler)
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

    public override void OnTriggerEnter2D(Collider2D collider2D)
    {
        base.OnTriggerEnter2D(collider2D);
    }

    public override void Tick()
    {
        owner.velocity.y += -15f * Time.deltaTime;

        if ((!owner.isFacingRight && controller2D.collisionInfo.left) || (owner.isFacingRight && controller2D.collisionInfo.right))
        {
            direction *= -1;
            owner.Flip();
        }


        owner.velocity.x = Mathf.SmoothDamp(owner.velocity.x, moveSpeed * direction, ref store, 0.15f);
    }

    protected override void HitReaction(Collider2D otherCollider)
    {
        if (otherCollider.GetComponent<PlayerController>() != null)
        {
            PlayerController player = otherCollider.GetComponent<PlayerController>();

            if (IsPlayerAbove(player.transform.position))
            {
                // Self take damage
            }
            else
            {
                player.TakeDamage(1);
            }

        }
    }

    private bool IsPlayerAbove(Vector2 playerPosition)
    {
        return (controller2D.transform.position.y < playerPosition.y);
    }

    protected override void Swap()
    {
        owner.EnemyFsm.ChangeCurrentState(0);
    }
}
