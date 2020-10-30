using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyState : State
{
    protected Enemy owner;
    protected Controller2D controller2D;
    protected HealthHandler healthHandler;

    public EnemyState(Enemy owner, Controller2D controller2D, HealthHandler healthHandler)
    {
        this.owner = owner;
        this.controller2D = controller2D;
        this.healthHandler = healthHandler;
    }

    public override void Enter()
    {
        healthHandler.OnHealthDepleted += Swap;

        controller2D.OnTriggerEnter += HitReaction;
    }

    public override void Exit()
    {
        healthHandler.OnHealthDepleted -= Swap;

        controller2D.OnTriggerEnter -= HitReaction;
    }

    public override void OnTriggerEnter2D(Collider2D collider2D)
    {
        throw new System.NotImplementedException();
    }

    public override void Tick()
    {

    }

    protected virtual void HitReaction(Collider2D otherCollider)
    { 
        
    }

    protected virtual void Swap()
    { 
        // Leave up to enemy state to define what to do after health is depelted.
    }
}
