using System;
using System.Collections;
using System.Collections.Generic;
using BaseGame;
using UnityEngine;

public class PowerUp : PickUp
{
    [SerializeField] protected int powerUpLevel;
    [SerializeField] protected SMTest.PlayerGrowthStates powerUpID;

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

        if (collider2D.GetComponent<SMTest.PlayerController>() != null)
        {
            SMTest.PlayerGrowth player = collider2D.GetComponent<SMTest.PlayerGrowth>();

            player.GrowTo(powerUpID);

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
