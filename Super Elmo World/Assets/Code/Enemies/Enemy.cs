using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Entity, IDamageable, IRanked, IBounceable
{

    // Target would be considered the closest player in some cases.
    protected Transform target;

    public void TakeDamage(int damageToTake)
    {

    }

    protected override void Awake()
    {
        base.Awake();
    }

    public virtual void Flip()
    {
        isFacingRight = !isFacingRight;

        Vector3 localScale = transform.localEulerAngles;
        localScale.y += 180;

        this.transform.localEulerAngles = localScale;
    }

    protected override void OnTriggerEnter2D(Collider2D collider)
    {
    

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

    public virtual void Promote(int level, int specifiedID)
    {

    }

    public virtual void Demote()
    {

    }

    public void Bounce(float launchHeight)
    {
        controller2D.SetVerticalForce(launchHeight);
    }
}
