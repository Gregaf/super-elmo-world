﻿using System.Collections;
using System.Collections.Generic;
using Unity.IO.LowLevel.Unsafe;
using UnityEngine;

public abstract class Projectile : MonoBehaviour
{
    public LayerMask whoToDamage;

    [SerializeField] protected float travelSpeed;
    [SerializeField] protected float bulletAcceleration;


    protected Vector2 velocity;
    protected Controller2D controller2D;
    protected BoxCollider2D bulletCollider2D;


    protected virtual void Awake()
    {
        controller2D = this.GetComponent<Controller2D>();
        bulletCollider2D = this.GetComponent<BoxCollider2D>();
    }

    protected virtual void OnTriggerEnter2D(Collider2D collision)
    {
        if (bulletCollider2D.IsTouchingLayers(whoToDamage))
        {
            if (collision.GetComponent<IDamageable>() != null)
            {
                collision.GetComponent<IDamageable>().TakeDamage(1);
                Destroy(this.gameObject);
            }
        }
    }
}
