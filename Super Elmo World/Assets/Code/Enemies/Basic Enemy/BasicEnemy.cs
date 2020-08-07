using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


// Review: A FSM could be used, but felt useless as the enemy is so simple.
public class BasicEnemy : Enemy
{
    [SerializeField] private float gravity = 20;
    [SerializeField] private float speed = 5;
    //private LayerMask detectionMask;
    private int moveDirection;

    protected override void Awake()
    {
        base.Awake();
    }

    protected override void OnDisable()
    {
        base.OnDisable();
    }

    protected override void OnEnable()
    {
        base.OnEnable();
    }

    protected override void Start()
    {
        base.Start();
        moveDirection = isFacingRight ? 1 : -1;

    }

    private void Update()
    {

        physicsController.SetHorizontalForce(Mathf.Lerp(physicsController.Velocity.x, speed * moveDirection, Time.deltaTime * 8));

        physicsController.SetVerticalForce(Mathf.Lerp(physicsController.Velocity.y, -gravity, Time.deltaTime));

        if ((physicsController.ControlState.isCollidingRight && isFacingRight) || (physicsController.ControlState.isCollidingLeft && !isFacingRight))
        {
            Flip();
            moveDirection = isFacingRight ? 1 : -1;
        }
    }

    protected override void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.GetComponent<PlayerBrain>() != null)
        {
            PlayerBrain currentPlayerBrain = collider.GetComponent<PlayerBrain>();

            if(currentPlayerBrain.transform.position.y <= transform.position.y)
            currentPlayerBrain.TakeDamage(1);
        }

    }

    protected override void Die(object o, EventArgs e)
    {
        base.Die(o, e);

    }
}
