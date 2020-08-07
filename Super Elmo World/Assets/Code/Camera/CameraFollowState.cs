using System;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollowState : IState
{
    private FSM ownerFsm;
    private float speed;
    private Transform[] targets;
    private Transform owner;
    private Vector3 vectorStore;
    private Vector3 offset = new Vector3(2, 2, 0);
    private bool active;

    public CameraFollowState(FSM ownerFsm, GameObject owner, Transform[] targets, float speed)
    {
        this.ownerFsm = ownerFsm;
        this.owner = owner.transform;
        this.targets = targets;
        this.speed = speed;
    }

    public void Enter()
    {
        PlayerManager.OnPlayerJoined += UpdatePlayers;
    }

    public void Exit()
    {
        PlayerManager.OnPlayerJoined -= UpdatePlayers;
    }

    public void StateUpdate()
    {
        if (targets.Length <= 0)
            return;

        Vector2 target = FindTargetPosition(targets);

        if (Vector2.Distance((owner.position), target) > .5f)
        {
            Vector3 newPosition = Vector3.SmoothDamp(owner.position, target, ref vectorStore, speed);
            newPosition.z = -10;
            owner.position = newPosition;

        }

    }

    // Review: Instead, will have to create a bounding box around all the players to represnt the camera view, and adjust camera based on that rect.
    private Vector2 FindTargetPosition(Transform[] targetArray)
    {
        int numberOfPlayers = targetArray.Length;
        Vector2 newPosition = Vector2.zero;

        // Review: I need to figure out how to sort via Distance to each
        //Array.Sort(targetArray);
        newPosition = (targetArray[0].position + targetArray[numberOfPlayers - 1].position) / 2;

        return newPosition;
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
