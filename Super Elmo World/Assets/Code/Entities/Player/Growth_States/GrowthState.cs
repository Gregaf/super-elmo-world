using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrowthState : State
{
    protected PlayerController playerController;
    protected HealthHandler healthHandler;
    protected float shieldAmount;

    public GrowthState(PlayerController playerController, float shieldAmount)
    {
        this.playerController = playerController;
        this.shieldAmount = shieldAmount;

        this.healthHandler = playerController.Health_Handler;
    }

    public override void Enter()
    {
        healthHandler.ResetHealth(shieldAmount);

        healthHandler.OnHealthDepleted += ShrinkTo;
    }

    public override void Exit()
    {
        healthHandler.OnHealthDepleted -= ShrinkTo;
    }

    public override void OnTriggerEnter2D(Collider2D collider2D)
    {
        throw new System.NotImplementedException();
    }

    public override void Tick()
    {

    }

    protected virtual void ShrinkTo()
    { 
        
    }
}
