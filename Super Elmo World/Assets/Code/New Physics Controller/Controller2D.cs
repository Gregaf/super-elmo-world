using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

[RequireComponent(typeof(BoxCollider2D), typeof(Rigidbody2D))]
public class Controller2D : MonoBehaviour
{

    public int horizontalRayCount = 4, verticalRayCount = 4;
    public CollisionInfo collisionInfo;
    public bool ignoreOneWayPlatformThisFrame;
    public bool invertYAxis = false;

    [SerializeField] private float maxClimbableSlopeAngle = 60f;
    [SerializeField] private float maxDescendableSlopeAngle = 60f;

    [SerializeField] private LayerMask collisionMask = 0;
    private LayerMask originalCollisionMask;
    [SerializeField] private LayerMask triggerMask = 0;
    [SerializeField] private LayerMask oneWayPlatformMask;
    private const float SKIN_WIDTH = 0.015f;
    private Rigidbody2D rb2d;
    private Transform objectTransform;
    private BoxCollider2D objectCollider;
    private RaycastOrigins raycastOrigins;

    private float horizontalRaySpacing, verticalRaySpacing;

    #region  Events/Properties
    public float MaxClimbableAngle {get {return maxClimbableSlopeAngle;}}
    public LayerMask CollisionMask {get{return collisionMask;}}


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

    /// <summary>
    /// Represents the points that the raycasts should originate from.
    /// </summary>
    private struct RaycastOrigins
    {
        public Vector2 topLeft, topRight;
        public Vector2 bottomLeft, bottomRight;
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

    void Awake()
    {
        rb2d = this.GetComponent<Rigidbody2D>();
        objectCollider = this.GetComponent<BoxCollider2D>();
        objectTransform = this.GetComponent<Transform>();

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

    private void Start()
    {
        CalculateRaySpacing(); 

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
        objectCollider.offset = new Vector2(0, newSize.y / 2);
           
        CalculateRaySpacing();
    }

    private float SlopeSpeedModifier(float angle)
    {


        return 0;
    }

    /// <summary>
    /// Resolves the collisions moving horizontally and vertically, then with the calculated values translates to velocity vector.
    /// </summary>
    /// <param name="velocity"> The amount of movement to be applied </param>
    public void Move(Vector2 velocity)
    {
        UpdateRaycastOrigins();

        if (!collisionInfo.wasGroundedLastFrame && collisionInfo.below)
        {
            OnLandedEvent?.Invoke();
            Debug.Log("Landed!");
        }

        if (collisionInfo.wasGroundedLastFrame && !collisionInfo.below && velocity.y < 0)
        {
            OnFellEvent?.Invoke();
            Debug.Log("Fell!");
        }

        collisionInfo.Reset();
    
        // Only go down a slope if we are not ascending in the y direction.
        if(velocity.y < 0)          
            DescendSlope(ref velocity);

            
        if(velocity.x != 0)
            HorizontalCollisionRes(ref velocity);

        //if(velocity.y != 0)
            VerticalCollisionRes(ref velocity);

        ignoreOneWayPlatformThisFrame = false;

        objectTransform.Translate(velocity);
    }

    /// <summary>
    /// Resolves collisions in X-axis movement using raycasts.
    /// </summary>
    /// <param name="velocity"></param>
    private void HorizontalCollisionRes(ref Vector2 velocity)
    {
        int currentXDirection = (int) Mathf.Sign(velocity.x);
        print("X dir: " + currentXDirection);
        float rayLength = Mathf.Abs(velocity.x) + SKIN_WIDTH;

        for(int i = 0; i < horizontalRayCount; i++)
        {
            Vector2 rayOrigin = (currentXDirection == -1) ? raycastOrigins.bottomLeft : raycastOrigins.bottomRight;
            rayOrigin += (Vector2.up * (horizontalRaySpacing * i)); 

            RaycastHit2D hit2D = Physics2D.Raycast(rayOrigin, Vector2.right * currentXDirection, rayLength, collisionMask);

            Debug.DrawRay(rayOrigin, Vector2.right * currentXDirection * rayLength, Color.red);
            
            if(hit2D)
            {
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
                        velocity.x -= flushDistance * currentXDirection;
                    }

                    AscendSlope(ref velocity, hitAngle);
                    
                    velocity.x += flushDistance * currentXDirection;
                }

                //
                if(!collisionInfo.ascendingSlope || hitAngle > maxClimbableSlopeAngle)
                {
                    velocity.x = (hit2D.distance - SKIN_WIDTH) * currentXDirection;
                    rayLength = hit2D.distance;

                    if(collisionInfo.ascendingSlope)
                    {
                        velocity.y = Mathf.Tan(collisionInfo.currentSlopeAngle * Mathf.Deg2Rad) * Mathf.Abs(velocity.x);

                    }

                    collisionInfo.right = currentXDirection == 1;
                    collisionInfo.left = currentXDirection == -1;
                }
            }

        }
    }

    
    private void AscendSlope(ref Vector2 velocity, float slopeAngle)
    {
        float moveDistance = Mathf.Abs(velocity.x);
        float climbVelocityY = Mathf.Sin(slopeAngle * Mathf.Deg2Rad) * moveDistance;

        // If velocity Y is lower then the object is trying to move upward so do not attach to the slope.
        if(velocity.y <= climbVelocityY)
        {
            velocity.y = Mathf.Sin(slopeAngle * Mathf.Deg2Rad) * moveDistance;
            velocity.x = Mathf.Cos(slopeAngle * Mathf.Deg2Rad) * moveDistance * Mathf.Sign(velocity.x);
            
            collisionInfo.below = true;
            collisionInfo.ascendingSlope = true;
            collisionInfo.currentSlopeAngle = slopeAngle;
        }
    }

    private void DescendSlope(ref Vector2 velocity)
    {
        int currentXDirection = (int) Mathf.Sign(velocity.x);

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
                    if(hit2D.distance - SKIN_WIDTH <= (Mathf.Tan(slopeAngle * Mathf.Deg2Rad) * Mathf.Abs(velocity.x)))
                    {
                        float moveDistance = Mathf.Abs(velocity.x);
                        float descendVelocityY = Mathf.Sin(slopeAngle * Mathf.Deg2Rad) * moveDistance;
                        print(descendVelocityY);

                        velocity.x = Mathf.Cos(slopeAngle * Mathf.Deg2Rad) * moveDistance * Mathf.Sign(velocity.x);
                        velocity.y -= descendVelocityY;

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
    /// <param name="velocity"></param>
    private void VerticalCollisionRes(ref Vector2 velocity)
    {
        if (invertYAxis)
            velocity.y *= -1;

        int currentYDirection = (int) Mathf.Sign(velocity.y);

        //if (invertYAxis)
            //currentYDirection *= -1;

        float rayLength = Mathf.Abs(velocity.y) + SKIN_WIDTH;

        LayerMask dynamicCollisionMask = collisionMask;

        if(currentYDirection == 1 || ignoreOneWayPlatformThisFrame)
            dynamicCollisionMask &= ~oneWayPlatformMask;

        for(int i = 0; i < verticalRayCount; i++)
        {
            Vector2 rayOrigin = (currentYDirection == -1) ? raycastOrigins.bottomLeft : raycastOrigins.topLeft;
            rayOrigin += (Vector2.right * (verticalRaySpacing * i + velocity.x)); 

            RaycastHit2D hit2D = Physics2D.Raycast(rayOrigin, Vector2.up * currentYDirection, rayLength, dynamicCollisionMask);

            Debug.DrawRay(rayOrigin, Vector2.up * currentYDirection * rayLength, Color.red);
            
            if(hit2D)
            {
                velocity.y = (hit2D.distance - SKIN_WIDTH) * currentYDirection;
                rayLength = hit2D.distance;

                // Adjusting x velocity so if collisions happen above while on slope
                if(collisionInfo.ascendingSlope)
                {
                    velocity.x = (velocity.y) / Mathf.Tan(collisionInfo.currentSlopeAngle * Mathf.Deg2Rad) * Mathf.Sign(velocity.x);
                }

                collisionInfo.above = invertYAxis ? currentYDirection == -1 : currentYDirection == 1;
                collisionInfo.below = invertYAxis ? currentYDirection == 1 : currentYDirection == -1;
            }
        }

        if(collisionInfo.ascendingSlope)
        {
            float xDirection = Mathf.Sign(velocity.x);
            rayLength = Mathf.Abs(velocity.x) + SKIN_WIDTH;
            Vector2 rayOrigin = (xDirection == -1) ? raycastOrigins.bottomLeft : raycastOrigins.bottomRight + Vector2.up * velocity.y; 
            RaycastHit2D hit2D = Physics2D.Raycast(rayOrigin, Vector2.right * xDirection, rayLength, collisionMask);

            if(hit2D)
            {
                float slopeAngle = Vector2.Angle(hit2D.normal, Vector2.up);

                if(slopeAngle != collisionInfo.currentSlopeAngle)
                {
                    velocity.x = (hit2D.distance - SKIN_WIDTH) * xDirection;

                    collisionInfo.currentSlopeAngle = slopeAngle;
                }

            }

        }
    }
    
    /// <summary>
    /// Bounds the amount of possible rays, and then calculates the space between each ray.
    /// </summary>
    private void CalculateRaySpacing()
    {
        Bounds bounds = objectCollider.bounds;
        bounds.Expand(SKIN_WIDTH * -2);

        horizontalRayCount = Mathf.Clamp(horizontalRayCount, 2, 30);
        verticalRayCount = Mathf.Clamp(verticalRayCount, 2, 30);

        horizontalRaySpacing = bounds.size.y / (horizontalRayCount - 1);
        verticalRaySpacing = bounds.size.x / (verticalRayCount - 1);
    }

    /// <summary>
    /// Sets the new rayOrigins based on the position fo the objects Colliders bounds.
    /// </summary>
    private void UpdateRaycastOrigins()
    {
        Bounds bounds = objectCollider.bounds;
        bounds.Expand(SKIN_WIDTH * -2);

        raycastOrigins.topLeft = new Vector2(bounds.min.x, bounds.max.y);
        raycastOrigins.topRight = bounds.max;
        raycastOrigins.bottomLeft = bounds.min;
        raycastOrigins.bottomRight = new Vector2(bounds.max.x, bounds.min.y);

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
