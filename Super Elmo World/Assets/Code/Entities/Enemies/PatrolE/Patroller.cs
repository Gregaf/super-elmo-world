using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Patroller : Enemy
{
    protected override void Awake()
    {
        base.Awake();
    }
    protected override void Start()
    {
        base.Start();

        EnemyFsm.AddToStateList(1, new PatrolWander(this, Control2D, Health_Handler));

        EnemyFsm.InitializeFSM(1);
    }

    protected override void OnTriggerEnter2D(Collider2D collider2D)
    {
        base.OnTriggerEnter2D(collider2D);
    }


    private void Update()
    {
        EnemyFsm.UpdateCurrentState();


        Control2D.Move(velocity * Time.deltaTime);
    }


}
