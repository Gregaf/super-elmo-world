using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AIInputHandler))]
public class Enemy : Entity, IDamageable, IBounceable
{

    // Target would be considered the closest player in some cases.
    public bool isVunerable { get; protected set; }
    public FSM EnemyFsm { get; private set; }

    public void TakeDamage(int damageToTake)
    {

    }

    protected override void Awake()
    {
        base.Awake();

        EnemyFsm = new FSM();

        isVunerable = true;
    }

    public virtual void Flip()
    {
        isFacingRight = !isFacingRight;

        Vector3 localEuler = transform.localEulerAngles;
        localEuler.y += 180;

        this.transform.localEulerAngles = localEuler;
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

    protected virtual void Die()
    {
        Debug.Log($"{gameObject} has died.");
        Destroy(gameObject);
    }

    public void Kill()
    {
        throw new NotImplementedException();
    }

    public void Bounce(float launchHeight)
    {
        
    }
}
