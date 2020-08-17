using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Entity, ITakeDamage
{
    // Target would be considered the closest player in some cases.
    protected Transform target;
    protected HealthManager health;

    public void TakeDamage(int damageToTake)
    {
        health.LoseHealth(damageToTake);
    }

    protected override void Awake()
    {
        base.Awake();
        health = this.GetComponent<HealthManager>();
    }

    protected override void OnEnable()
    {
        health.OnEntityDie += Die;
    }

    protected override void OnDisable()
    {
        health.OnEntityDie -= Die;
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

            if (playerTouched.IsMovingDown() && playerTouched.transform.position.y > transform.position.y)
            {
                playerTouched.Bounce();
                TakeDamage(1);
                Debug.Log("Take dat Damage.");
            }

        }

    }

    protected virtual void Die()
    {
        Debug.Log($"{gameObject} has died.");
        Destroy(gameObject);
    }

}
