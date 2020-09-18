using UnityEngine;

public class ControllerState2D
{
    // List of bools for collision detection possibilities
    public bool isCollidingRight { get; set; }
    public bool isCollidingLeft { get; set; }
    public bool isCollidingAbove { get; set; }
    public bool isCollidingBelow { get; set; }
    public bool isMovingDownSlope { get; set; }
    public bool isMovingUpSlope { get; set; }
    public bool isGrounded { get { return isCollidingBelow; } }
    public float slopeAngle { get; set; }
    public Vector2 oldDeltaMove {get; set;}

    public bool isOnSlope { get; set; }

    public bool HasCollisions{ get { return isCollidingAbove || isCollidingBelow || isCollidingLeft || isCollidingRight; } }

    // Reset the booleans by setting them all to false.
    public void Reset()
    {
        isOnSlope =
        isMovingUpSlope =
        isMovingDownSlope =
        isCollidingAbove =
        isCollidingBelow =
        isCollidingLeft =
        isCollidingRight = false;

        slopeAngle = 0;
    }

    // Return the current state of each boolean as a nicely formatted string.
    public override string ToString()
    {
        return string.Format("(Controller: r:{0} | l:{1} | a:{2} | b:{3} | down-slope:{4} | up-slope:{5} | angle:{6} | OnSlope: {7})",
            isCollidingRight,
            isCollidingLeft,
            isCollidingAbove,
            isCollidingBelow,
            isMovingDownSlope,
            isMovingUpSlope,
            slopeAngle,
            isOnSlope);
    }

}
