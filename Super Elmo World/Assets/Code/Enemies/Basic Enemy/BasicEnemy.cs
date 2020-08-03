using System.Collections;
using System.Collections.Generic;
using UnityEngine;


// Review: A FSM could be used, but felt useless as the enemy is so simple.
public class BasicEnemy : Enemy
{
    private float gravity;
    private float speed;
    private LayerMask detectionMask;
    // (1 : -1) -> (Right : Left)
    private int moveDirection;

    protected override void Awake()
    {
        base.Awake();
    }

    protected override void OnDisable()
    {
    }

    protected override void OnEnable()
    {
    }

    protected override void OnTriggerEnter2D(Collider2D collider)
    {
        base.OnTriggerEnter2D(collider);
    }

    protected override void Start()
    {
        base.Start();
    }

    public void Update()
    {
        
    }
}
