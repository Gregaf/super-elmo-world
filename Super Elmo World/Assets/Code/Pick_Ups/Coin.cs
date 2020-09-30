using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : PickUp
{

    protected override void Start()
    {
        base.Start();
        pickUpName = "Coin";
        description = "A coin that can increase lives after collecting enough.";

    }

    protected override void AttractionState()
    {
        // Play coin spinning animation.
    }

    protected override void Update()
    {
        base.Update();
    }

    protected override void OnTriggerEnter2D(Collider2D collider2D)
    {
        base.OnTriggerEnter2D(collider2D);
    }
}
