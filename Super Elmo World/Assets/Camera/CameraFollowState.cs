using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class CameraFollowState : IState
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

    public void Enter()
    {
        PlayerManager.Instance.OnPlayerJoined += UpdatePlayers;

        offset = new Vector3(2, 0, 0);
    }

    public void Exit()
    {
        PlayerManager.Instance.OnPlayerJoined -= UpdatePlayers;
    }

    public void StateUpdate()
    {
        if (targets.Length <= 0)
            return;

        MoveCamera();
        ZoomCamera();
        

    }

    private void ZoomCamera()
    {
        float newZoom = Mathf.Lerp(minimumZoomDistance, maximumZoomDistance, GetGreatestDistance() / 50);

        gameCamera.orthographicSize = Mathf.Lerp(gameCamera.orthographicSize, newZoom, Time.deltaTime * 3);
    }

    private void MoveCamera()
    {
        Vector3 target = GetCenterPoint() + offset;

        Vector2 camBounds = GetCameraExtents();

        target.y = Mathf.Clamp(target.y, levelBounds.bounds.min.y + camBounds.y, levelBounds.bounds.max.y - camBounds.y);
        target.x = Mathf.Clamp(target.x, levelBounds.bounds.min.x + camBounds.x, levelBounds.bounds.max.x - camBounds.x);
        target.z = -10;

        owner.position = Vector3.SmoothDamp(owner.position, target, ref vectorStore, speed);
    }

    private Vector2 GetCameraExtents()
    {
        Vector2 bounds = Vector2.zero;
        bounds.y = gameCamera.orthographicSize;
        bounds.x = (bounds.y) * gameCamera.aspect;
        

        return bounds;
    }


    private float GetGreatestDistance()
    {
        Bounds newBounds = new Bounds(targets[0].position, Vector3.zero);

        for (int i = 0; i < targets.Length; i++)
        {
            newBounds.Encapsulate(targets[i].position);
        }

        return newBounds.size.x;
    }

    private Vector3 GetCenterPoint()
    {
        if (targets.Length == 1)
            return targets[0].position;

        Bounds newBounds = new Bounds(targets[0].position, Vector3.zero);

        for (int i = 0; i < targets.Length; i++)
        {
            newBounds.Encapsulate(targets[i].position);
        }

        return newBounds.center;
    }

    private void UpdatePlayers()
    {
        PlayerBrain[] players = GameObject.FindObjectsOfType<PlayerBrain>();

        Array.Resize(ref targets, players.Length);

        for (int i = 0; i < players.Length; i++)
        {
            targets[i] = players[i].GetComponent<Transform>();
        }
    }

}
