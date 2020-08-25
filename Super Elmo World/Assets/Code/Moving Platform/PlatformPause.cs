using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class PlatformPause : IState
{
    private FSM ownerFsm;
    private GameObject platform;

    private string platformType;
    private float pauseTime;

    private float timer;

    public PlatformPause(FSM ownerFsm, GameObject platform, PlatformType platformType, float pauseTime)
    {
        this.ownerFsm = ownerFsm;
        this.platform = platform;
        this.platformType = platformType.ToString();
        this.pauseTime = pauseTime;
    }

    public void Enter()
    {
        timer = Time.time + pauseTime;
    }

    public void Exit()
    {

    }

    public void OnTriggerEnter(Collider2D collider2D)
    {
        throw new System.NotImplementedException();
    }

    public void Tick()
    {
        
        if(timer <= Time.time)
            ownerFsm.ChangeCurrentState(ownerFsm.GetState(platformType));
    }
}
