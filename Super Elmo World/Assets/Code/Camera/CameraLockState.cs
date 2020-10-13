using UnityEngine;

public class CameraLockState : State
{
    private CameraFollower owner;
    private Transform lockPoint;

    public CameraLockState(CameraFollower owner, Transform lockPoint)
    {
        this.owner = owner;
        this.lockPoint = lockPoint;
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
        Vector3 targetPoint = lockPoint.position;


        owner.MoveCamera(1f, Vector3.zero, targetPoint);

        owner.ZoomCamera(4f, 2f);
    }
}
