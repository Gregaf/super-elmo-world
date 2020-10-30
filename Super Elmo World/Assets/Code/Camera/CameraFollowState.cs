using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using TMPro;
using UnityEngine;

public class CameraFollowState : State
{
    private FSM ownerFsm;
    private float speed;
    private List<Transform> targets;
    private CameraFollower owner;
    private Vector3 offset = Vector3.zero;
    private bool active;

    public CameraFollowState(CameraFollower owner, List<Transform> targets, float speed, Vector3 offset)
    {
        this.owner = owner;
        this.targets = targets;
        this.speed = speed;
        this.offset = offset;
    }

    public void OnTriggerEnter(Collider2D collider2D)
    {
    }

    public override void Enter()
    {

    }

    public override void Exit()
    {

    }

    public override void Tick()
    {
        Vector3 targetPoint = owner.GetCenterPoint();


        
        owner.MoveCamera(speed, offset, targetPoint);     
    }

    public override void OnTriggerEnter2D(Collider2D collider2D)
    {
        throw new NotImplementedException();
    }
}
