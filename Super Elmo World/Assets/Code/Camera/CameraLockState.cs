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
        Debug.Log($"{ToString()}, Entered.");


    }

    public void Exit()
    {
        Debug.Log($"{ToString()}, Exited.");
    }

    public void StateUpdate()
    {

    }
}
