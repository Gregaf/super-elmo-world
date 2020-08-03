using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Entity, ITakeDamage
{
    // Target would be considered the closest player in some cases.
    protected Transform target;


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

    protected override void Start()
    {
        base.Start();
    }

    protected override void OnTriggerEnter2D(Collider2D collider)
    {
        // Do some stuff to check if a Player mask has been touched...
        // so... if(collider.GetComponent<ITakeDamage>() != null)
        // Then it can take damage, verify if its player and call target.TakeDmg.

    }

    public void TakeDamage(int damageToTake)
    {
        healthManager.LoseHealth(damageToTake);

    }
}
