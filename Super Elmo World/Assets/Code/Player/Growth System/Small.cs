using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Small : IState
{
    private PlayerBrain playerBrain;
    private Vector3 playerScale;


    public Small(PlayerBrain playerBrain)
    {
        this.playerBrain = playerBrain;

        playerScale = new Vector3(.75f, .5f, 1f);
    }

    public void Enter()
    {
        // Switch to small sprite...
        playerBrain.ModifySize(playerScale);
    }

    public void Exit()
    {
        
    }

    public void StateUpdate()
    {
        // May not need anything as of now since the small state has no extra functionality.
    }
}
