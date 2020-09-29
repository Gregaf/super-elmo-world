using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

[RequireComponent(typeof(BoxCollider2D), typeof(Rigidbody2D))]
public class Controller2D : RayCastController
{

    public CollisionInfo collisionInfo;
    public bool ignoreOneWayPlatformThisFrame;
    
    [SerializeField] private float maxClimbableSlopeAngle = 60f;
    [SerializeField] private float maxDescendableSlopeAngle = 60f;
    [SerializeField] private LayerMask collisionMask = 0;
    private LayerMask originalCollisionMask;
    [SerializeField] private LayerMask triggerMask = 0;
    [SerializeField] private LayerMask oneWayPlatformMask;
    private bool invertYAxis = false;

    [SerializeField] private Transform aestheticTransform;

    private float ignoreSlopeTime = 1f;
    private float ignoreTimer = 0f;

    #region  Events/Properties
    public bool YAxisIsInverted { get { return invertYAxis; } }
    public float MaxClimbableAngle {get {return maxClimbableSlopeAngle;}}
    public LayerMask CollisionMask {get{return collisionMask;}}


    public event Action OnYAxisInverted;
    public event Action OnLandedEvent;
    public event Action OnFellEvent;
    public event Action<Collider2D> OnTriggerEnter;
    public event Action<Collider2D> OnTriggerStay;
    public event Action<Collider2D> OnTriggerExit;
    #endregion

    /// <summary>
    /// Stores information about the collisions that happen.
    /// </summary>
    public struct CollisionInfo
    {
        public bool above, below, right, left;
        public bool ascendingSlope, descendingSlope, isOnSlope;

        public float currentSlopeAngle, previousSlopeAngle;

        public bool isGrounded {get {return below;}}
        public bool wasGroundedLastFrame;
        public void Reset()
        {
            wasGroundedLastFrame = below;
            previousSlopeAngle = currentSlopeAngle;
            currentSlopeAngle = 0;
            
            descendingSlope =
            ascendingSlope = 
            above =
            below = 
            right = 
            left  = false;
        }
    }

    private Rect windowRect = new Rect(20, 20, 200, 200);

    private void OnGUI()
    {
        windowRect = GUI.Window(0, windowRect, DoStuff, "My Window");
    }

    private void DoStuff(int windowID)
    {
        string belowString = "Below: " + collisionInfo.below.ToString(); 
        GUI.Label(new Rect(25, 25, 120, 20), belowString);

        string aboveString = "Above: " + collisionInfo.above.ToString();
        GUI.Label(new Rect(25, 45, 120, 20), aboveString);

        string leftString = "Left: " + collisionInfo.left.ToString();
        GUI.Label(new Rect(25, 65, 120, 20), leftString);

        string rightString = "Right: " + collisionInfo.right.ToString();
        GUI.Label(new Rect(25, 85, 120, 20), rightString);

        string upSlopeString = "Up Slope: " + collisionInfo.ascendingSlope.ToString();
        GUI.Label(new Rect(25, 105, 150, 20), upSlopeString);

        string downSlopeString = "Down Slope: " + collisionInfo.descendingSlope.ToString();
        GUI.Label(new Rect(25, 130, 150, 20), downSlopeString);

        string onSlopeString = "On Slope: " + collisionInfo.isOnSlope.ToString();
        GUI.Label(new Rect(25, 155, 150, 20), onSlopeString);

        string slopeAngle = "Slope Angle: " + collisionInfo.currentSlopeAngle.ToString();
        GUI.Label(new Rect(25, 180, 150, 20), slopeAngle);
    }

    protected override void Awake()
    {
        base.Awake();

        // 0000 | 0010 ----> 0010 so adds the one way platfrom masks to collision mask.
        collisionMask |= oneWayPlatformMask;

        originalCollisionMask = collisionMask;

        for(int i = 0; i < 32; i++)
        {
            if((triggerMask.value & 1 << i) == 0)
            {
                Physics2D.IgnoreLayerCollision(gameObject.layer, i);
            }

        }

    }

    protected override void Start()
    {
        base.Start();


    }

    public void EnableCollisions()
    {
        collisionMask = originalCollisionMask;
    }

    public void DisableCollisions()
    {
        collisionMask &= 0;
    }
    public void ModifySize(Vector2 newSize)
    {
        objectCollider.size = newSize;

        if (YAxisIsInverted)
            newSize.y *= -1;

        objectCollider.offset = new Vector2(0, newSize.y / 2);

        aestheticTransform.localPosition = objectCollider.offset;

        CalculateRaySpacing();
    }

    private void CorrectBoxColliderOffset()
    {
        objectCollider.offset *= -1;
        aestheticTransform.localPosition = objectCollider.offset;

    }

    private float SlopeSpeedModifier(float angle)
    {


        return 0;
    }

    public void InvertYAxis()
    {
        invertYAxis = !invertYAxis;
        CorrectBoxColliderOffset();
        OnYAxisInverted?.Invoke();
    }

    /// <summary>
    /// Resolves the collisions moving horizontally and vertically, then with the calculated values translates to velocity vector.
    /// </summary>
    /// <param name="deltaMovement"> The amount of movement to be applied </param>
    public void Move(Vector2 deltaMovement, bool standingOnPlatform = false)
    {
        UpdateRaycastOrigins();

        if (!collisionInfo.wasGroundedLastFrame && collisionInfo.below)
        {
            OnLandedEvent?.Invoke();
            // Debug.Log("Landed!");
        }

        if (collisionInfo.wasGroundedLastFrame && !collisionInfo.below && deltaMovement.y < 0)
        {
            OnFellEvent?.Invoke();
            // Debug.Log("Fell!");
        }

        collisionInfo.Reset();
    
        // Only go down a slope if we are not ascending in the y direction.
        if(deltaMovement.y < 0)          
            DescendSlope(ref deltaMovement);

            
        if(deltaMovement.x != 0)
            HorizontalCollisionRes(ref deltaMovement);

        //if(velocity.y != 0)
            VerticalCollisionRes(ref deltaMovement);

        ignoreSlopeTime -= Time.deltaTime;

        ignoreOneWayPlatformThisFrame = false;

        objectTransform.Translate(deltaMovement);

        if (standingOnPlatform)
            collisionInfo.below = true;

    }

    /// <summary>
    /// Resolves collisions in X-axis movement using raycasts.
    /// </summary>
    /// <param name="deltaMovement"></param>
    private void HorizontalCollisionRes(ref Vector2 deltaMovement)
    {
        int currentXDirection = (int) Mathf.Sign(deltaMovement.x);
        float rayLength = Mathf.Abs(deltaMovement.x) + SKIN_WIDTH;

        for(int i = 0; i < horizontalRayCount; i++)
        {
            Vector2 rayOrigin = (currentXDirection == -1) ? raycastOrigins.bottomLeft : raycastOrigins.bottomRight;
            rayOrigin += (Vector2.up * (horizontalRaySpacing * i)); 

            RaycastHit2D hit2D = Physics2D.Raycast(rayOrigin, Vector2.right * currentXDirection, rayLength, collisionMask);

            Debug.DrawRay(rayOrigin, Vector2.right * currentXDirection * rayLength, Color.red);
            
            if(hit2D)
            {
                if (hit2D.distance == 0)
                    continue;

                Vector2 otherVec = !invertYAxis ? Vector2.up : Vector2.down;

                float hitAngle = Vector2.Angle(hit2D.normal, otherVec);

                // The first ray will be first to hit a slope.
                int startingRay = invertYAxis ? horizontalRayCount - 1 : 0;

                if(i == startingRay && hitAngle <= maxClimbableSlopeAngle)
                {
                    
                    float flushDistance = 0;

                    // Moves the object to be flush with the slope.
                    if(collisionInfo.currentSlopeAngle != collisionInfo.previousSlopeAngle)
                    {
                        flushDistance = hit2D.distance - SKIN_WIDTH;
                        deltaMovement.x -= flushDistance * currentXDirection;
                    }

                    AscendSlope(ref deltaMovement, hitAngle);
                    
                    deltaMovement.x += flushDistance * currentXDirection;
                }

                //
                if(!collisionInfo.ascendingSlope || hitAngle > maxClimbableSlopeAngle)
                {
                    deltaMovement.x = (hit2D.distance - SKIN_WIDTH) * currentXDirection;
                    rayLength = hit2D.distance;

                    if(collisionInfo.ascendingSlope)
                    {
                        deltaMovement.y = Mathf.Tan(collisionInfo.currentSlopeAngle * Mathf.Deg2Rad) * Mathf.Abs(deltaMovement.x);

                    }

                    collisionInfo.right = currentXDirection == 1;
                    collisionInfo.left = currentXDirection == -1;
                }
            }

        }
    }

    
    private void AscendSlope(ref Vector2 deltaMovement, float slopeAngle)
    {
        float moveDistance = Mathf.Abs(deltaMovement.x);
        float climbVelocityY = Mathf.Sin(slopeAngle * Mathf.Deg2Rad) * moveDistance;

        if (deltaMovement.y <= (climbVelocityY))
        {
            Debug.Log($"Velocity: {deltaMovement.y}, Climb Velocity: {climbVelocityY}");
            deltaMovement.y = Mathf.Sin(slopeAngle * Mathf.Deg2Rad) * moveDistance;
            deltaMovement.x = Mathf.Cos(slopeAngle * Mathf.Deg2Rad) * moveDistance * Mathf.Sign(deltaMovement.x);

            collisionInfo.below = true;
            collisionInfo.ascendingSlope = true;
            collisionInfo.currentSlopeAngle = slopeAngle;
        }
        else
        {
            //ignoreTimer = ignoreSlopeTime;
            // Start a timer to ingore attatch to slope for few frames.
        }
    }

    private void DescendSlope(ref Vector2 deltaMovement)
    {
        int currentXDirection = (int) Mathf.Sign(deltaMovement.x);

        Vector2 rayOrigin = currentXDirection == -1 ? raycastOrigins.bottomRight : raycastOrigins.bottomLeft;
        Vector2 rayDir = invertYAxis ? Vector2.up : Vector2.down;

        if (invertYAxis)     
            rayOrigin = currentXDirection == -1 ? raycastOrigins.topRight : raycastOrigins.topLeft;
        

        RaycastHit2D hit2D = Physics2D.Raycast(rayOrigin, rayDir, Mathf.Infinity, collisionMask);
        
        if(hit2D)
        {
            Debug.DrawRay(rayOrigin, rayDir * 10f, Color.magenta);

            float slopeAngle = Vector2.Angle(hit2D.normal, rayDir * Vector2.down);

            collisionInfo.isOnSlope = slopeAngle != 0 ? true : false;

            if(slopeAngle != 0 && slopeAngle < maxDescendableSlopeAngle)
            {
                if(Mathf.Sign(hit2D.normal.x) == currentXDirection)
                {
                    if(hit2D.distance - SKIN_WIDTH <= (Mathf.Tan(slopeAngle * Mathf.Deg2Rad) * Mathf.Abs(deltaMovement.x)))
                    {
                        float moveDistance = Mathf.Abs(deltaMovement.x);
                        float descendVelocityY = Mathf.Sin(slopeAngle * Mathf.Deg2Rad) * moveDistance;
                        print(descendVelocityY);

                        deltaMovement.x = Mathf.Cos(slopeAngle * Mathf.Deg2Rad) * moveDistance * Mathf.Sign(deltaMovement.x);
                        deltaMovement.y -= descendVelocityY;

                        collisionInfo.below = true;
                        collisionInfo.descendingSlope = true;
                        collisionInfo.currentSlopeAngle = slopeAngle;
                    }
                }
            }

        }

    }

    /// <summary>
    /// Resolves collisions in Y-Axis movement using raycasts.
    /// </summary>
    /// <param name="deltaMovement"></param>
    private void VerticalCollisionRes(ref Vector2 deltaMovement)
    {
        if (invertYAxis)
            deltaMovement.y *= -1;

        int currentYDirection = (int) Mathf.Sign(deltaMovement.y);

        //if (invertYAxis)
            //currentYDirection *= -1;

        float rayLength = Mathf.Abs(deltaMovement.y) + SKIN_WIDTH;

        LayerMask dynamicCollisionMask = collisionMask;

        if(currentYDirection == 1 || ignoreOneWayPlatformThisFrame)
            dynamicCollisionMask &= ~oneWayPlatformMask;

        for(int i = 0; i < verticalRayCount; i++)
        {
            Vector2 rayOrigin = (currentYDirection == -1) ? raycastOrigins.bottomLeft : raycastOrigins.topLeft;
            rayOrigin += (Vector2.right * (verticalRaySpacing * i + deltaMovement.x)); 

            RaycastHit2D hit2D = Physics2D.Raycast(rayOrigin, Vector2.up * currentYDirection, rayLength, dynamicCollisionMask);

            Debug.DrawRay(rayOrigin, Vector2.up * currentYDirection * rayLength, Color.red);
            
            if(hit2D)
            {
                deltaMovement.y = (hit2D.distance - SKIN_WIDTH) * currentYDirection;
                rayLength = hit2D.distance;

                // Adjusting x velocity so if collisions happen above while on slope
                if(collisionInfo.ascendingSlope)
                {
                    deltaMovement.x = (deltaMovement.y) / Mathf.Tan(collisionInfo.currentSlopeAngle * Mathf.Deg2Rad) * Mathf.Sign(deltaMovement.x);
                }

                collisionInfo.above = invertYAxis ? currentYDirection == -1 : currentYDirection == 1;
                collisionInfo.below = invertYAxis ? currentYDirection == 1 : currentYDirection == -1;
            }
        }

        if(collisionInfo.ascendingSlope)
        {
            float xDirection = Mathf.Sign(deltaMovement.x);
            rayLength = Mathf.Abs(deltaMovement.x) + SKIN_WIDTH;
            Vector2 rayOrigin = (xDirection == -1) ? raycastOrigins.bottomLeft : raycastOrigins.bottomRight + Vector2.up * deltaMovement.y; 
            RaycastHit2D hit2D = Physics2D.Raycast(rayOrigin, Vector2.right * xDirection, rayLength, collisionMask);

            if(hit2D)
            {
                float slopeAngle = Vector2.Angle(hit2D.normal, Vector2.up);

                if(slopeAngle != collisionInfo.currentSlopeAngle)
                {
                    deltaMovement.x = (hit2D.distance - SKIN_WIDTH) * xDirection;

                    collisionInfo.currentSlopeAngle = slopeAngle;
                }

            }

        }
    }   

    private void OnTriggerEnter2D(Collider2D otherCollider)
    {
        OnTriggerEnter?.Invoke(otherCollider);
    }
    private void OnTriggerStay2D(Collider2D otherCollider)
    {
        OnTriggerStay?.Invoke(otherCollider);
    }
    private void OnTriggerExit2D(Collider2D otherCollider)
    {
        OnTriggerExit?.Invoke(otherCollider);
    }

}
