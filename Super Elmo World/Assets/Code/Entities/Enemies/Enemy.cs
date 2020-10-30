using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Entity, IDamageable, IBouncer
{
    
    public bool isVunerable { get; set; }
    public FSM EnemyFsm { get; private set; }
    public HealthHandler Health_Handler { get { return _healthHandler; }}


    [SerializeField] private HealthHandler _healthHandler;

    public void TakeDamage(int damageToTake)
    {
        _healthHandler.LoseHealth(damageToTake);
        // Maybe try invoking an event for the state to subscribe to
        // This would allow each state to determine how to handle taking damage.
        // If the player jumps on a spiked enemy not vunerable. Then take damage would be ignored
        // Otherwise it would cause damage appropriately.

        // Or should the Call be avoided all together?
        // Can we avoid the call without knowing exactly what we are jumping on?

        //_healthHandler.LoseHealth(damageToTake);
    }

    protected override void Awake()
    {
        base.Awake();

        _healthHandler = new HealthHandler();

        EnemyFsm = new FSM();

        isVunerable = true;
    }

    protected override void Start()
    {
        base.Start();

        EnemyFsm.AddToStateList(0, new EDead(this));

    }

    public virtual void Flip()
    {
        isFacingRight = !isFacingRight;

        SpriteRenderer.flipX = !SpriteRenderer.flipX;
    }

    public void OnPlayerCol(Collider2D col)
    { 
        // Check to make sure its a player
        // Then we can pass information about the collision so each state can handle the information seperrately?
    }

    protected override void OnTriggerEnter2D(Collider2D collider2D)
    {
        //EnemyFsm.CurrentState.OnTriggerEnter2D(collider2D);

    }

    public void Kill()
    {
        Debug.Log($"{gameObject} has died.");
        Destroy(gameObject);
    }

    public void Bounce(float launchHeight)
    {
        velocity.y = launchHeight;
    }
}
