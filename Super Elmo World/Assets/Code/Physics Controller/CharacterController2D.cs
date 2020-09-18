using System;
using UnityEngine;
using UnityEngine.UI;

[AddComponentMenu("Entity/CharacterController2D")]
[RequireComponent(typeof(Rigidbody2D))]
public class CharacterController2D : MonoBehaviour
{
    private const float skinWidth = 0.015f;
    private const int totalHorizontalRays = 8;
    private const int totalVerticalRays = 8;

    private static readonly float slopeLimitTangent = Mathf.Tan(75 * Mathf.Deg2Rad);

    public LayerMask platformMask = 0;
    public LayerMask triggerMask = 0;

    public LayerMask oneWayPlatformMask;

    public ControllerState2D ControlState { get; private set; }
    public Vector2 Velocity { get { return entityVelocity; } }
    public bool HandleCollision { get; set; }
    // '??' is null coalescing operator, it returns the second condition if the first is null
    public GameObject StandingOn { get; private set; }
    public Vector2 PlatformVelocity { get; private set; }

    private Vector2 entityVelocity;
    private Transform entityTransform;
    private BoxCollider2D entityBoxCollider;
    private float jumpIn;

    bool wasGrounded;

    private Vector2 activeLocalPlatformPoint, activeGlobalPlatformPoint;

    private float verticalDistanceBetweenRays, horizontalDistanceBetweenRays;

    private Vector2 raycastTopLeft;
    private Vector2 raycastBottomRight;
    private Vector2 raycastBottomLeft;

    public event Action<Collider2D> OnTriggerEnterEvent;
    public event Action<Collider2D> OnTriggerStayEvent;
    public event Action<Collider2D> OnTriggerExitEvent;

    private Rect windowRect = new Rect(20, 20, 200, 200);

    private void OnGUI()
    {
        windowRect = GUI.Window(0, windowRect, DoStuff, "My Window");
    }

    private void DoStuff(int windowID)
    {
        string belowString = "Below: " + ControlState.isCollidingBelow.ToString(); 
        GUI.Label(new Rect(25, 25, 120, 20), belowString);

        string aboveString = "Above: " + ControlState.isCollidingAbove.ToString();
        GUI.Label(new Rect(25, 45, 120, 20), aboveString);

        string leftString = "Left: " + ControlState.isCollidingLeft.ToString();
        GUI.Label(new Rect(25, 65, 120, 20), leftString);

        string rightString = "Right: " + ControlState.isCollidingRight.ToString();
        GUI.Label(new Rect(25, 85, 120, 20), rightString);

        string upSlopeString = "UpSlope: " + ControlState.isMovingUpSlope.ToString();
        GUI.Label(new Rect(25, 105, 150, 20), upSlopeString);

        string downSlopeString = "DownSlope: " + ControlState.isMovingDownSlope.ToString();
        GUI.Label(new Rect(25, 130, 150, 20), downSlopeString);
    }

    private void Awake()
    {
        ControlState = new ControllerState2D();
        entityTransform = this.transform;

        if (this.GetComponent<BoxCollider2D>() != null)
            entityBoxCollider = this.GetComponent<BoxCollider2D>();

        CalculateRayBounds(); 

        HandleCollision = true;

    }

    public void AddForce(Vector2 force)
    {
        entityVelocity += force;
    }

    public void SetForce(Vector2 force)
    {
        entityVelocity = force;
    }

    public void SetHorizontalForce(float xMove)
    {
        entityVelocity.x = xMove;
    }

    public void SetVerticalForce(float yMove)
    {
        entityVelocity.y = yMove;
    }

    private void LateUpdate()
    {
        
        Move(Velocity * Time.deltaTime);
    }

    public void ModifySize(Vector2 newSize)
    {
        entityBoxCollider.size = newSize;
        entityBoxCollider.offset = new Vector2(0, newSize.y / 2);
           
        CalculateRayBounds();
    }

    private void Move(Vector2 deltaMovement)
    {
        // To keep track if we were grounded last frame
        wasGrounded = ControlState.isCollidingBelow;

        ControlState.Reset();

        if (HandleCollision)
        {
            
            //HandleMovingPlatforms();
            CalculateRayOrigins();   

            if (deltaMovement.x != 0)
                MoveHorizontally(ref deltaMovement); 

            if ((deltaMovement.y != 0))
                MoveVertically(ref deltaMovement);

        }

        entityTransform.Translate(deltaMovement, Space.World);

        if (Time.deltaTime > 0)
            entityVelocity = deltaMovement / Time.deltaTime;

        //entityVelocity.x = Mathf.Clamp(entityVelocity.x, Parameters.maxVelocity.x * -1, Parameters.maxVelocity.x);
        //entityVelocity.y = Mathf.Clamp(entityVelocity.y, Parameters.maxVelocity.y * -1, Parameters.maxVelocity.y);

        if (ControlState.isMovingUpSlope)
            entityVelocity.y = 0;
    }

    public void CalculateRayOrigins()
    {
        Bounds calculatedBounds = entityBoxCollider.bounds;
        calculatedBounds.Expand(-2 * skinWidth);

        raycastTopLeft = new Vector2 (calculatedBounds.min.x, calculatedBounds.max.y);
        raycastBottomRight = new Vector2(calculatedBounds.max.x, calculatedBounds.min.y);
        raycastBottomLeft = calculatedBounds.min;
    }

    private void MoveHorizontally(ref Vector2 deltaMovement)
    {
        bool isGoingRight = deltaMovement.x > 0;
        float rayDistance = Mathf.Abs(deltaMovement.x) + skinWidth;
        Vector2 rayDirection = isGoingRight ? Vector2.right : Vector2.left;
        Vector2 rayOrigin = isGoingRight ? raycastBottomRight : raycastBottomLeft;

        for (int i = 0; i < totalHorizontalRays; i++)
        {
            Vector2 rayVector = new Vector2(rayOrigin.x, rayOrigin.y + (i * verticalDistanceBetweenRays));
            Debug.DrawRay(rayVector, rayDirection * rayDistance, Color.red);

            RaycastHit2D rayCastHit = Physics2D.Raycast(rayVector, rayDirection, rayDistance, platformMask);

            if (rayCastHit)           
            {
                // float flushDistance = Mathf.Sign(deltaMovement.x) * (rayCastHit.distance - skinWidth);
                // transform.Translate(new Vector2(flushDistance, 0));
                
                if(i == 0)
                {
                    
                }
                

                deltaMovement.x = rayCastHit.point.x - rayVector.x;
                rayDistance = Mathf.Abs(deltaMovement.x);
                
                if (isGoingRight)
                {
                    deltaMovement.x -= skinWidth;
                    ControlState.isCollidingRight = true;
                }
                else
                {
                    deltaMovement.x += skinWidth;
                    ControlState.isCollidingLeft = true;
                }

                if (rayDistance < skinWidth + 0.001f)
                {
                    break;
                }
            }

        }

    }

    private void MoveVertically(ref Vector2 deltaMovement)
    {
        bool isGoingUp = deltaMovement.y > 0;
        float rayDistance = Mathf.Abs(deltaMovement.y) + skinWidth;
        Vector2 rayDirection = isGoingUp ? Vector2.up : Vector2.down;
        Vector2 rayOrigin = isGoingUp ? raycastTopLeft : raycastBottomLeft;

        rayOrigin.x += deltaMovement.x;

        for (int i = 0; i < totalVerticalRays; i++)
        {
            Vector2 rayVector = new Vector2(rayOrigin.x + (i * horizontalDistanceBetweenRays), rayOrigin.y);
            Debug.DrawRay(rayVector, rayDirection * rayDistance, Color.red);

            RaycastHit2D rayCastHit = Physics2D.Raycast(rayVector, rayDirection, rayDistance, platformMask);

            // If the ray has not hit anything, then skip the rest of function.
            if (!rayCastHit)            
                continue;

            deltaMovement.y = rayCastHit.point.y - rayVector.y;
            rayDistance = Mathf.Abs(deltaMovement.y);

            if (isGoingUp)
            {
                deltaMovement.y -= skinWidth;
                ControlState.isCollidingAbove = true;

            }
            else
            {
                deltaMovement.y += skinWidth;
                ControlState.isCollidingBelow = true;
            }

            if (rayDistance < skinWidth + 0.0001f)
                break;
        }

    }

    // Calculate the distance bewtween the horizontal and vertical rays
    public void CalculateRayBounds()
    {
        float colliderHeight = entityBoxCollider.size.y * Mathf.Abs(entityTransform.localScale.y) - (2 * skinWidth);
        verticalDistanceBetweenRays = colliderHeight / (totalHorizontalRays - 1);

        float colliderWidth = entityBoxCollider.size.x * Mathf.Abs(entityTransform.localScale.x) - (2 * skinWidth);
        horizontalDistanceBetweenRays = colliderWidth / (totalVerticalRays - 1);
    }


    public void OnTriggerEnter2D(Collider2D otherCol)
    {
        OnTriggerEnterEvent?.Invoke(otherCol);
    }

    public void OnTriggerStay2D(Collider2D otherCol)
    {
        OnTriggerStayEvent?.Invoke(otherCol);
    }

    public void OnTriggerExit2D(Collider2D otherCol)
    {
        OnTriggerExitEvent?.Invoke(otherCol);
    }
}
