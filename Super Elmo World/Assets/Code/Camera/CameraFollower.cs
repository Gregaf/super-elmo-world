﻿using UnityEngine;

public class CameraFollower : MonoBehaviour
{
    [SerializeField] private float speed = 5;
    private FSM cameraFsm;


    private void Awake()
    {
        cameraFsm = new FSM();
        PlayerBrain[] targets = FindObjectsOfType<PlayerBrain>();
        int length = targets.Length;
        Transform[] targetPositions = new Transform[length];

        for (int i = 0; i < length; i++)
        {
            targetPositions[i] = targets[i].transform;
        }

        cameraFsm.AddToStateList("Camera_Follow",new CameraFollowState(cameraFsm, this.gameObject, targetPositions, speed));
        cameraFsm.AddToStateList("Camera_Lock", new CameraLockState(cameraFsm, this.gameObject));

        cameraFsm.InitializeFSM(cameraFsm.GetState("Camera_Follow"));

        cameraFsm.isActive = true;
    }

    private void Update()
    {
        cameraFsm.UpdateCurrentState();
    }

}
