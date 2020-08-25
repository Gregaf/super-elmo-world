using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BaseGame;

public class KeyFollow : IState
{
    private Key owner;
    private FSM ownerFsm;
    private Vector2 offSet = new Vector2(-1, 0);
    private float followSpeed;
    private LayerMask check;
    private KeyDoor targetDoor;

    private PlayerController targetPlayer;
    private Transform targetPlayerTransform;
    private Vector2 followPosition;
    private Transform ownerTransform;

    private Vector2 tempVector;

    public KeyFollow(Key owner, KeyDoor targetDoor, FSM ownerFsm, Vector2 offSet, float followSpeed , LayerMask check)
    {
        this.owner = owner;
        this.targetDoor = targetDoor;
        this.ownerFsm = ownerFsm;
        this.offSet = offSet;
        this.followSpeed = followSpeed;
        this.check = check;

        ownerTransform = owner.gameObject.transform;
    }

    public void Enter()
    {
        GetFollowTarget();

        targetPlayer.OnPlayerDeath += ChangeToReposition;
    }

    public void Exit()
    {

        targetPlayer.OnPlayerDeath -= ChangeToReposition;
    }

    public void Tick()
    {
        followPosition = AdjustFollowPosition(targetPlayer.isFacingRight);

        if (Vector2.Distance(ownerTransform.position, targetPlayerTransform.position) > 0.1f)
        {
            ownerTransform.position = Vector2.SmoothDamp(ownerTransform.position, followPosition, ref tempVector, followSpeed);
        }

        if (Vector2.Distance(ownerTransform.position, targetDoor.transform.position) < 8f)
        {
            ownerFsm.ChangeCurrentState("Use");
        }
        
    }


    private Vector2 AdjustFollowPosition(bool facingRight)
    {
        Vector2 newFollowPostion = Vector2.zero;

        if (facingRight)
        {
            newFollowPostion = (Vector2)targetPlayerTransform.position - offSet;
            newFollowPostion.y += offSet.y * 2;

            return newFollowPostion;
        }
        else
        {
            newFollowPostion = (Vector2)targetPlayerTransform.position + offSet;

            return newFollowPostion;
        }
    }

    private void GetFollowTarget()
    {
        Collider2D col = Physics2D.OverlapBox(ownerTransform.position, new Vector2(1.5f, 0.75f), 0, check);

        if (col.GetComponent<PlayerController>() != null)
        {
            targetPlayer = col.GetComponent<PlayerController>();

            targetPlayerTransform = targetPlayer.transform;
        }

        Debug.Log($"Collided with {col.gameObject}");
    }

    private void ChangeToReposition()
    {
        ownerFsm.ChangeCurrentState("Idle");
    }

    public void OnTriggerEnter(Collider2D collider2D)
    {
        throw new System.NotImplementedException();
    }
}
