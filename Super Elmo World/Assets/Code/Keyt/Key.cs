using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Key : MonoBehaviour, IRespawnable
{

    [SerializeField] private KeyDoor targetDoor;
    [SerializeField] private LayerMask whoCanGrab;

    [Header("Idle Variables")]
    [SerializeField] private float fallSpeed = -10f;

    [Header("Follow Variables")]
    [SerializeField] private Vector2 offSet = Vector2.zero;
    [SerializeField] private float followSpeed = 0.5f;

    [Header("Use Variables")]
    public float travelTime;

    private FSM keyFsm;
    private CharacterController2D controller2D;
    private BoxCollider2D BoxCollider2D;
    private Vector2 startingPoint;

    public event Action OnReachedDoor;

    private void Awake()
    {
        keyFsm = new FSM();
        BoxCollider2D = this.GetComponent<BoxCollider2D>();
        controller2D = this.GetComponent<CharacterController2D>();


    }

    private void Start()
    {
        startingPoint = transform.position;

        keyFsm.AddToStateList("Idle", new KeyIdle(keyFsm, controller2D, BoxCollider2D, whoCanGrab, fallSpeed));
        keyFsm.AddToStateList("Follow", new KeyFollow(this, targetDoor, keyFsm, offSet, followSpeed, whoCanGrab));
        //keyFsm.AddToStateList("Reposition", new KeyReposition());
        keyFsm.AddToStateList("Use", new KeyGetUsed(this, targetDoor, controller2D, travelTime));

        keyFsm.InitializeFSM(keyFsm.GetState("Idle"));
    }

    private void Update()
    {
        keyFsm.UpdateCurrentState();

        Debug.Log(keyFsm.IsCurrentState("Follow"));
    }

    public void Respawn()
    {
        transform.position = startingPoint;
    }
}
