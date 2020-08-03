using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBrain : Entity, ITakeDamage
{


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
    
    public void TakeDamage(int damageToTake)
    {


    }

    protected override void OnTriggerEnter2D(Collider2D collider)
    {

    }
}
