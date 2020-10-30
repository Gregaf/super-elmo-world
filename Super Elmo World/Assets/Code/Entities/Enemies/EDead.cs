using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;


public class EDead : State
{
    private float fallSpeed = 20;
    private Enemy owner;

    public static event Action OnEnemyDeath;

    public EDead(Enemy owner)
    {
        this.owner = owner;

    }

    public override void Enter()
    {
        OnEnemyDeath?.Invoke();

        owner.velocity = Vector2.zero;
        owner.velocity.y = 5f;
    }

    public override void Exit()
    {
        // Reset enemy then deactivate?
    }

    public override void OnTriggerEnter2D(Collider2D collider2D)
    {
    }

    public override void Tick()
    {
        owner.velocity.y += -fallSpeed * Time.deltaTime;



    }
}
