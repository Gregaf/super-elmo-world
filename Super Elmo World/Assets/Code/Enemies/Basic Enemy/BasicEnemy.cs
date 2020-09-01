using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicEnemy : Enemy
{
    [Header("Normal State")]
    [SerializeField] private float gravity = 20;
    [SerializeField] private float speed = 5;

    private FSM basicEnemyFsm;

    protected override void Awake()
    {
        base.Awake(); 
        
        basicEnemyFsm = new FSM();
    }

    protected override void Start()
    {
        base.Start();

        basicEnemyFsm.AddToStateList("Normal", new BNormal(basicEnemyFsm, controller2D, this, speed, gravity));
        basicEnemyFsm.AddToStateList("Dead", new EDead(this.gameObject));

        basicEnemyFsm.InitializeFSM(basicEnemyFsm.GetState("Normal"));
    }

    private void Update()
    {
        basicEnemyFsm.UpdateCurrentState();

    }

    protected override void OnTriggerEnter2D(Collider2D collider)
    {
        basicEnemyFsm.CurrentState.OnTriggerEnter(collider);
    }

    protected override void Die()
    {
        base.Die();

    }
}
