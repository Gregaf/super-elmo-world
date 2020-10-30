using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class Dead : State
{
    private PlayerController player;
    private Camera gameCamera; // Want to get my screen size to determine once the player has left the camera.
    // Maybe there is a way to have the camera raise event whenever a player leaves the camera bounds. Then dead players will handle that accordingly?
    private Bounds cameraBounds;

    private float fallTime = 3f;
    private float timer = 0f;

    public static event Action<PlayerData> OnPlayerDied;


    public Dead(PlayerController player)
    {
        this.player = player;
        
    }

    public override void Enter()
    {
        player.PlayerInputHandler.DisableControls();
        OnPlayerDied?.Invoke(player.Player_Data);
        player.Control2D.DisableCollisions();

        // Disable collisions.
        // Disable listening for collision events.
        // Set y velocity to an arbitrary bounce height for death effect.
        player.baseMovementFSM.isActive = false;

        player.velocity = Vector2.zero;
        player.velocity.y = 8f;

        timer = 0;
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
        player.velocity.y += -15f * Time.deltaTime;

        timer += Time.deltaTime;

        if (timer > fallTime)
        {
            if (player.Player_Data.GetLives() > 0)
            {
                player.Player_Data.LoseLives(1);

                ChangeToBubble();
            }
            else
            {
                StayDead();
            }
        }

    }

    private void ChangeToBubble()
    {
        player.growthFsm.ChangeCurrentState((int)PlayerGrowthStates.BUBBLE);
    }

    private void StayDead()
    {
        Debug.Log($"<color=cyan>{player.gameObject}</color> has run out of lives!");  
    }

}
