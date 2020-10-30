using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rocker : Enemy
{
    public Transform target;
    public LayerMask whoToSearchFor;
    public GameObject projectile;

    private Searching SearchingState;
    public Hide HidingState;
    public Attacking AttackingState;
    private KnockedOut KnockedOutState;

    protected override void Awake()
    {
        base.Awake();
    }
    protected override void Start()
    {
        base.Start();
        SearchingState = new Searching(this, target, whoToSearchFor);
        HidingState = new Hide();
        AttackingState = new Attacking(this, projectile);
        KnockedOutState = new KnockedOut();

        EnemyFsm.InitializeFSM(SearchingState);
    }

    protected override void OnTriggerEnter2D(Collider2D collider2D)
    {
        base.OnTriggerEnter2D(collider2D);
    }

    private void Update()
    {
        EnemyFsm.UpdateCurrentState();
    }
}
