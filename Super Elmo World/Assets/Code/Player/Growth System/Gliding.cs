using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gliding : IState
{
    private PlayerBrain playerBrain;

    private Vector3 playerScale;

    public Gliding(PlayerBrain playerBrain)
    {
        this.playerBrain = playerBrain;

        playerScale = new Vector3(1, 2, 1);
    }

    public void Enter()
    {
        playerBrain.ModifySize(playerScale);
    }

    public void Exit()
    {
    }

    public void StateUpdate()
    {
    }
}
