using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class PlayerMoveProperties
{
    public PlayerMoveProperties()
    {
        
    }

    public float currentSpeed;

    [Header("Wall Jump Variables")]
    public float wallSlideSpeed;
    public Vector2 wallLaunchVelocity;

    [Header("Ground")]
    [Space(20)]
    public float walkSpeed;
    public float runSpeed;
    public float maxRunSpeed;
    public float runAcceleration;
    public float groundAccelerationTime;
    
    [Header("Air")]
    [Space(20)]
    public float airAccelerationTime;
    public float jumpHeight;
    public float minimumJumpHeight;
    public float jumpTime;
    public float gravityScale;
    public float graceTime;

    public float minimumJumpTime { get; private set; }

    public float jumpVelocity { get; private set; }
    public float normalGravity { get; private set; }
    public float minimumJumpVelocity { get; private set; }
    

    [HideInInspector] public float dynamicGravity;
    
    public void CalculatePhysicsValues()
    {
        jumpVelocity = (2 * jumpHeight) / jumpTime;
        normalGravity = -jumpVelocity / jumpTime;
        minimumJumpVelocity = Mathf.Sqrt((jumpVelocity * jumpVelocity) + 2 * normalGravity * (jumpHeight - minimumJumpHeight));
        minimumJumpTime = jumpTime - (2 * (jumpHeight - minimumJumpHeight) / (jumpVelocity + minimumJumpVelocity));

        dynamicGravity = normalGravity;
    }

}
