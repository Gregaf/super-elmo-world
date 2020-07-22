using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class PlatformPause : State
{
    private string platformType;
    private float pauseTime;
    private GameObject platform;

    private float timer;

    public PlatformPause(FSM ownerFsm, GameObject platform, PlatformType platformType, float pauseTime)
    {
        this.ownerFsm = ownerFsm;
        this.platform = platform;
        this.platformType = platformType.ToString();
        this.pauseTime = pauseTime;
    }

    public override void Enter()
    {
        timer = Time.time + pauseTime;
    }

    public override void Exit()
    {

    }

    public override void StateUpdate()
    {
        
        if(timer <= Time.time)
            ownerFsm.ChangeCurrentState(ownerFsm.GetState(platformType));
    }
}
