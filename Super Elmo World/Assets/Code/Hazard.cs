#undef DEBUG

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D), typeof(Collider2D))]
public class Hazard : MonoBehaviour
{
    [SerializeField] private float damage;
    [SerializeField] private LayerMask whoToDamage;
    [SerializeField] private Collider2D hazardCollider2D;

    protected virtual void Start()
    {
        hazardCollider2D = GetComponent<Collider2D>();

    }

    protected virtual void OnTriggerEnter2D(Collider2D collision)
    {
        if (hazardCollider2D.IsTouchingLayers(whoToDamage) && collision.GetComponent<IDamageable>() != null)
        {
            collision.GetComponent<IDamageable>().TakeDamage((int) damage);

#if DEBUG
            Debug.Log($"<color=blue>{this.gameObject}</color> caused <color=red>{damage}</color> damage to <color=yellow>{collision.gameObject}</color>");
#endif
        }

    }

}
