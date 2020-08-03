using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CameraLockState : IState
{
    private FSM ownerFsm;

    public CameraLockState(FSM ownerFsm, GameObject owner)
    {
        this.ownerFsm = ownerFsm;

    }
    public void Enter()
    {
        Debug.Log(ToString() + " Enter.");


    }

    public void Exit()
    {
        Debug.Log(ToString() + " Exit.");
    }

    public void StateUpdate()
    {

    }
}
