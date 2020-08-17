using System;
using UnityEngine;

public class GameState : IState
{
    private FSM ownerFsm;
    private float levelTimer;

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


    }

    private void RemoveDeadPlayer(int playerIndex)
    { 
        
    }

}
