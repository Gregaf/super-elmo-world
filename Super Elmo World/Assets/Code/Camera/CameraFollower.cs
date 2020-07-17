using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollower : MonoBehaviour
{
    FSM cameraFsm;
    public Transform target;
    public float speed;

    private void Awake()
    {
        cameraFsm = new FSM();

        cameraFsm.AddToStateList("Camera_Follow",new CameraFollowState(cameraFsm, this.gameObject, target, speed));
        cameraFsm.AddToStateList("Camera_Lock", new CameraLockState(cameraFsm, this.gameObject));

        cameraFsm.InitializeFSM(cameraFsm.GetState("Camera_Follow"));

        cameraFsm.isActive = true;
    }

    public void Update()
    {
        cameraFsm.UpdateCurrentState();
    }

}
