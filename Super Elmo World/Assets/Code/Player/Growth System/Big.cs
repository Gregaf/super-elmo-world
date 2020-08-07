using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Big : IState
{
    private PlayerBrain playerBrain;

    private Vector3 playerScale;

    public Big(PlayerBrain playerBrain)
    {
        this.playerBrain = playerBrain;

        playerScale = new Vector3(1f, 1f, 1f);
    }

    public void Enter()
    {
        playerBrain.ModifySize(playerScale);
        // Maybe playerBrain.EnableStrongerHead(); so that way bigger blocks can be broken.
    }

    public void Exit()
    {
    }

    public void StateUpdate()
    {
    }
}
