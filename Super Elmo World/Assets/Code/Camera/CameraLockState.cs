using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CameraLockState : State
{


    public CameraLockState(FSM ownerFsm, GameObject owner)
    {
        this.ownerFsm = ownerFsm;

    }
    public override void Enter()
    {
        Debug.Log(ToString() + " Enter.");


    }

    public override void Exit()
    {
        Debug.Log(ToString() + " Exit.");
    }

    public override void StateUpdate()
    {

    }
}
