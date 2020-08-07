using System;
using UnityEngine;

public class GameState : IState
{
    private FSM ownerFsm;
    private float levelTimer;

    public event Action<float> updateTimeCallback;

    public GameState(FSM ownerFsm, float levelTimer)
    {
        this.ownerFsm = ownerFsm;
        this.levelTimer = levelTimer;
    }

    public void Enter()
    {

    }

    public void Exit()
    {

    }

    public void StateUpdate()
    {
        //levelTimer -= Time.deltaTime;

        //updateTimeCallback(levelTimer);
    }
}
