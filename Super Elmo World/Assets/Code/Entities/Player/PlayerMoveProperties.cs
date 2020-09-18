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

    [Header("Ground Variables")]
    public float walkSpeed;
    public float runSpeed;
    public float maxRunSpeed;
    public float runAcceleration;
    public float groundAcceleration;

    [Header("Air Variables")]
    public float jumpHeight;
    public float jumpTime;
    public float airAcceleration;
    [HideInInspector] public float jumpVelocity;
    [HideInInspector] public float dynamicGravity;
    public float baseGravity;
    public float fallMultiplier;

    [Header("Swimming Variables")]
    public float swimSpeed;
    public float paddleVelocity;
    public float swimGravity;
    public float swimAcceleration;
    public float swimJumpInterval;

}
