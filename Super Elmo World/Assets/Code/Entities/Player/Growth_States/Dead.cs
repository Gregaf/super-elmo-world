using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class Dead : State
{
    private PlayerController player;

    public event Action OnPlayerDied;

    public Dead(PlayerController player)
    {

    }

    public override void Enter()
    {
        OnPlayerDied?.Invoke();
        player.controller2D.DisableCollisions();
        
        // Disable collisions.
        // Disable listening for collision events.
        // Set y velocity to an arbitrary bounce height for death effect.
        // Maybe reset x velocity.
    }

    public override void Exit()
    {

    }

    public override void OnTriggerEnter2D(Collider2D collider2D)
    {
        throw new System.NotImplementedException();
    }

    public override void Tick()
    {
        // Apply a negative y velocity for falling.
        // OnExiting camera bounds -> if Lives > 0 then transition to bubble, else disable player.

    }
}
