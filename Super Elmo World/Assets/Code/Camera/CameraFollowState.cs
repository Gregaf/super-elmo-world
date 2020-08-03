using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollowState : IState
{
    private FSM ownerFsm;
    private float speed;
    private Transform target;
    private GameObject owner;
    private Vector3 vectorStore;
    private Vector3 offset = new Vector3(2, 2,0);

    public CameraFollowState(FSM ownerFsm, GameObject owner, Transform target, float speed)
    {
        this.ownerFsm = ownerFsm;
        this.owner = owner;
        this.target = target;
        this.speed = speed;
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
        if (Vector2.Distance((owner.transform.position), target.position) > .5f)
        {
            Vector3 newPosition = Vector3.SmoothDamp(owner.transform.position, target.position, ref vectorStore, speed);
            newPosition.z = -10;
            owner.transform.position = newPosition;

        }

    }

}
