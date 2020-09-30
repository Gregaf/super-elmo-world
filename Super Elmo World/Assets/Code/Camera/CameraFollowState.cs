using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using TMPro;
using UnityEngine;

public class CameraFollowState : State
{
    private FSM ownerFsm;
    private float speed;
    private Transform[] targets;
    private Transform owner;
    private Vector3 vectorStore;
    private Vector3 offset = new Vector3(0, 0, 0);
    private bool active;
    private BoxCollider2D levelBounds;

    float minimumZoomDistance = 10f;
    float maximumZoomDistance = 12f;
    private Camera gameCamera;
    

    public CameraFollowState(FSM ownerFsm, GameObject owner, Transform[] targets, float speed, BoxCollider2D levelBounds)
    {
        this.ownerFsm = ownerFsm;
        this.owner = owner.transform;
        this.targets = targets;
        this.speed = speed;
        this.levelBounds = levelBounds;

        gameCamera = GameObject.FindObjectOfType<Camera>();
    }


    private void MoveCamera()
    {
        
        Vector3 target = offset;

        //Vector2 camBounds = GetCameraExtents();

        target.z = -10;
        owner.position = Vector3.SmoothDamp(owner.position, new Vector3((int) target.x, (int) target.y, target.z), ref vectorStore, speed);
    }

    public void OnTriggerEnter(Collider2D collider2D)
    {
    }

    public override void Enter()
    {
        throw new NotImplementedException();
    }

    public override void Exit()
    {
        throw new NotImplementedException();
    }

    public override void Tick()
    {
        throw new NotImplementedException();
    }

    public override void OnTriggerEnter2D(Collider2D collider2D)
    {
        throw new NotImplementedException();
    }
}
