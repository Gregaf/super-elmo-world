using UnityEngine;

public class CameraLockState : State
{
    private FSM ownerFsm;

    public CameraLockState(FSM ownerFsm, GameObject owner)
    {
        this.ownerFsm = ownerFsm;

    }

    public override void Enter()
    {

    }

    public override void Exit()
    {

    }

    public void OnTriggerEnter(Collider2D collider2D)
    {

    }

    public override void OnTriggerEnter2D(Collider2D collider2D)
    {

    }

    public override void Tick()
    {

    }
}
