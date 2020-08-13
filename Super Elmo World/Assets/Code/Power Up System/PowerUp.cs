using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUp : PickUp
{
    [SerializeField] protected int powerUpLevel;
    [SerializeField] protected int powerUpID;

    protected override void Start()
    {
        base.Start();
    }

    protected override void AttractionState()
    {
        base.AttractionState();
    }

    protected override void ExpirationState()
    {
        base.ExpirationState();
    }

    protected override void OnTriggerEnter2D(Collider2D collider2D)
    {
        base.OnTriggerEnter2D(collider2D);

        if (collider2D.GetComponent<PlayerBrain>() != null)
        {
            HealthManager playerHealth = collider2D.GetComponent<HealthManager>();

            playerHealth.SetHealthAndID(powerUpLevel, powerUpID);

            Destroy(gameObject);
        }

    }

    protected override void PayLoadState()
    {
        base.PayLoadState();
    }


    protected override void Update()
    {
        base.Update();
    }
}
