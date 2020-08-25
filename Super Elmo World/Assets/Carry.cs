using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Carry : MonoBehaviour
{
    public bool isCarryingObject = false;
    public Vector2 carryOffset;

    GameObject carriedGO;
    ILiftable carriedObject; 


    private Entity entity;

    private void Awake()
    {
        entity = this.GetComponent<Entity>();


    }

    private void Update()
    {
        if (carriedGO == null)
            return;

        // Adjust the position of carried object, and the orientation based on entity facingDirection.
        Vector2 followPosition = AdjustFollowPosition(!entity.isFacingRight);

        carriedGO.transform.position = followPosition;
    }
    private Vector2 AdjustFollowPosition(bool facingRight)
    {
        Vector2 newFollowPostion = Vector2.zero;

        if (facingRight)
        {
            newFollowPostion = (Vector2) transform.position - carryOffset;
            newFollowPostion.y += carryOffset.y * 2;

            return newFollowPostion;
        }
        else
        {
            newFollowPostion = (Vector2) transform.position + carryOffset;

            return newFollowPostion;
        }
    }

    public void PickUpLiftable(ILiftable objectToLift)
    {
        carriedObject = objectToLift;
        
        isCarryingObject = true;

        carriedGO = objectToLift.Lift();
    }

    public void LaunchLiftable(Vector2 launchDirection, float launchSpeed)
    {
        carriedObject.Release(launchDirection * launchSpeed);
        carriedGO = null;
        carriedObject = null;
        isCarryingObject = false;
    }
    


}
