using System.Collections;
using System.Collections.Generic;
using Unity.IO.LowLevel.Unsafe;
using UnityEngine;

public abstract class Projectile : MonoBehaviour
{
    public LayerMask whoToDamage;

    protected float travelSpeed;
    protected float bulletAcceleration;

    protected CharacterController2D controller2D;
    private BoxCollider2D bulletCollider2D;

    protected virtual void Awake()
    {
        controller2D = this.GetComponent<CharacterController2D>();
        bulletCollider2D = this.GetComponent<BoxCollider2D>();
    }

    public virtual void Launch()
    { 
        
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
