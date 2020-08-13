using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Entity, ITakeDamage
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

    protected override void OnEnable()
    {
    }

    protected override void OnDisable()
    {
    }

    protected virtual void Flip()
    {
        isFacingRight = !isFacingRight;

        Vector3 localScale = transform.localEulerAngles;
        localScale.y += 180;

        this.transform.localEulerAngles = localScale;
    }

    protected override void OnTriggerEnter2D(Collider2D collider)
    {
        if(collider.GetComponent<PlayerBrain>() != null)
        {
            PlayerBrain playerTouched = collider.GetComponent<PlayerBrain>();

            if(playerTouched.isMovingDown && playerTouched.transform.position.y > transform.position.y)
            {
                TakeDamage(1);
                Debug.Log("Take dat Damage.");
            }

        }

    }

    protected virtual void Die(System.Object o, EventArgs e)
    {
        Debug.Log($"{gameObject} has died.");
    }

}
