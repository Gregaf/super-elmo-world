using System;
using UnityEngine;
[Serializable]
public class ControllerParameters
{
    // Allow for change of Jump behavior depending on a 2D Volume
    public enum JumpBehavior
    {
        CanJumpOnGround,
        CanJumpAnywhere,
        CantJump
    }

    // The maximum and value for the entity to move, by default is max value of a float
    public Vector2 maxVelocity = new Vector2(float.MaxValue, float.MaxValue);
    
    [Range(0, 90)]
    public float slopeLimit = 30;
    public JumpBehavior JumpRestrictions;

    // The percentage of the maximum jump height 
    [Range(0,1)]
    public float minimumJumpPercentage;

    public float gravity;



}
