﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Entity, ITakeDamage
{
    // Target would be considered the closest player in some cases.
    protected Transform target;


    public void TakeDamage(int damageToTake)
    {
        healthManager.LoseHealth(damageToTake);

    }

    protected override void Awake()
    {
        base.Awake();

    }

    protected override void OnEnable()
    {
        healthManager.OnDeathCallback += Die;
    }

    protected override void OnDisable()
    {
        healthManager.OnDeathCallback -= Die;
    }

    protected virtual void Flip()
    {
        isFacingRight = !isFacingRight;

        Vector3 localScale = transform.localEulerAngles;
        localScale.y += 180;

        this.transform.localEulerAngles = localScale;
    }

    protected virtual void Die(System.Object o, EventArgs e)
    {
        Debug.Log($"{gameObject} has died.");
    }

}
