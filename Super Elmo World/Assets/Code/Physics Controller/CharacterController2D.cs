using System;
using UnityEngine;

[AddComponentMenu("Entity/CharacterController2D")]
[RequireComponent(typeof(Rigidbody2D))]
public class CharacterController2D : MonoBehaviour
{
    private const float skinWidth = 0.003f;
    private const int totalHorizontalRays = 4;
    private const int totalVerticalRays = 4;

    private static readonly float slopeLimitTangent = Mathf.Tan(75f * Mathf.Deg2Rad);

    public LayerMask platformMask;

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

    private Vector2 activeLocalPlatformPoint, activeGlobalPlatformPoint;

    private float verticalDistanceBetweenRays, horizontalDistanceBetweenRays;

    private Vector2 raycastTopLeft;
    private Vector2 raycastBottomRight;
    private Vector2 raycastBottomLeft;

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
        jumpIn -= Time.deltaTime;

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
        bool wasGrounded = ControlState.isCollidingBelow;
        ControlState.Reset();

        if (HandleCollision)
        {
            
            HandleMovingPlatforms();
            CalculateRayOrigins();

            if (deltaMovement.y < 0 && wasGrounded)
                HandleVerticalSlope(ref deltaMovement);


            MoveHorizontally(ref deltaMovement);

            MoveVertically(ref deltaMovement);
        }

        entityTransform.Translate(deltaMovement, Space.World);

        if (Time.deltaTime > 0)
            entityVelocity = deltaMovement / Time.deltaTime;

        //entityVelocity.x = Mathf.Clamp(entityVelocity.x, Parameters.maxVelocity.x * -1, Parameters.maxVelocity.x);
        //entityVelocity.y = Mathf.Clamp(entityVelocity.y, Parameters.maxVelocity.y * -1, Parameters.maxVelocity.y);

        if (ControlState.isMovingUpSlope)
            entityVelocity.y = 0;

        if (StandingOn != null)
        {
            activeGlobalPlatformPoint = transform.position;
            activeLocalPlatformPoint = StandingOn.transform.InverseTransformPoint(transform.position);
        }
    }

    // Handle the entity's velocity depending on the platform's velocity.
    private void HandleMovingPlatforms()
    {
        if (StandingOn != null)
        {
            Vector2 newGlobalPlatformPoint = StandingOn.transform.TransformPoint(activeLocalPlatformPoint);
            Vector2 moveDistance = newGlobalPlatformPoint - activeGlobalPlatformPoint;

            // If the distance between the entity and the platform is not zero, move the entity the distance difference.
            if (moveDistance != Vector2.zero)
            {
                transform.Translate(moveDistance, Space.World);
            }

            PlatformVelocity = (newGlobalPlatformPoint - activeGlobalPlatformPoint) / Time.deltaTime;
        }
        else
        {
            PlatformVelocity = Vector2.zero;
        }

        StandingOn = null;
    }

    public void CalculateRayOrigins()
    {
        Vector2 size = new Vector2(entityBoxCollider.size.x * Mathf.Abs(transform.localScale.x),
                                   entityBoxCollider.size.y * Mathf.Abs(transform.localScale.y)) / 2;
        Vector2 center = new Vector2(entityBoxCollider.offset.x * transform.localScale.x, entityBoxCollider.offset.y * transform.localScale.y);

        raycastTopLeft = (Vector2) entityTransform.position + new Vector2(center.x - size.x + skinWidth, center.y + size.y - skinWidth);
        raycastBottomRight = (Vector2) entityTransform.position + new Vector2(center.x + size.x - skinWidth, center.y - size.y + skinWidth);
        raycastBottomLeft = (Vector2) entityTransform.position + new Vector2(center.x - size.x + skinWidth, center.y - size.y + skinWidth);
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
            Debug.DrawRay(rayVector, rayDirection * rayDistance, Color.yellow);

            RaycastHit2D rayCastHit = Physics2D.Raycast(rayVector, rayDirection, rayDistance, platformMask);

            if (!rayCastHit)           
                continue;

            if (i == 0 && HandleHorizontalSlope(ref deltaMovement, Vector2.Angle(rayCastHit.normal, Vector2.up), isGoingRight))
                break;

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

            if (rayDistance < skinWidth + 0.0001f)
                break;
        }

    }

    private void MoveVertically(ref Vector2 deltaMovement)
    {
        bool isGoingUp = deltaMovement.y > 0;
        float rayDistance = Mathf.Abs(deltaMovement.y) + skinWidth;
        Vector2 rayDirection = isGoingUp ? Vector2.up : Vector2.down;
        Vector2 rayOrigin = isGoingUp ? raycastTopLeft : raycastBottomLeft;

        rayOrigin.x += deltaMovement.x;

        float standingOnDistance = float.MaxValue;
        for (int i = 0; i < totalVerticalRays; i++)
        {
            Vector2 rayVector = new Vector2(rayOrigin.x + (i * horizontalDistanceBetweenRays), rayOrigin.y);
            Debug.DrawRay(rayVector, rayDirection * rayDistance, Color.yellow);

            RaycastHit2D rayCastHit = Physics2D.Raycast(rayVector, rayDirection, rayDistance, platformMask);

            // If the ray has not hit anything, then skip the rest of function.
            if (!rayCastHit)            
                continue;

            if (!isGoingUp)
            {
                float verticalDistanceToHit = entityTransform.position.y - rayCastHit.point.y;
                if (verticalDistanceToHit < standingOnDistance)
                {
                    standingOnDistance = verticalDistanceToHit;
                    StandingOn = rayCastHit.collider.gameObject;
                }
            }

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

            if (!isGoingUp && deltaMovement.y > 0.001f)
            {
                ControlState.isMovingUpSlope = true;          
            }

            if (rayDistance < skinWidth + 0.0001f)
                break;
        }

    }

    private void HandleVerticalSlope(ref Vector2 deltaMovement)
    {
        float center = (raycastBottomLeft.x + raycastBottomRight.x) / 2;
        Vector2 direction = Vector2.down;
        float slopeDistance = slopeLimitTangent * (raycastBottomRight.x - center);
        Vector2 slopeRayVector = new Vector2(center, raycastBottomLeft.y);

        Debug.DrawRay(slopeRayVector, direction * slopeDistance, Color.magenta);

        RaycastHit2D rayCastHit = Physics2D.Raycast(slopeRayVector, direction, slopeDistance, platformMask);

        bool isMovingDownSlope = Mathf.Sign(rayCastHit.normal.x) == Mathf.Sign(deltaMovement.x);

        if (!rayCastHit)
            return;

        float angle = Vector2.Angle(rayCastHit.normal, Vector2.up);

        if (Mathf.Abs(angle) < 0.0001f)
            return;
        
        ControlState.isMovingDownSlope = true;
        ControlState.slopeAngle = angle;

        deltaMovement.y = rayCastHit.point.y - slopeRayVector.y;

    }
    private bool HandleHorizontalSlope(ref Vector2 deltaMovement, float angle, bool isGoingRight)
    {
        if (Mathf.RoundToInt(angle) == 90)
            return false;

        if (angle > 85)
        {
            deltaMovement.x = 0;
            return true;
        }

        if (deltaMovement.y > .07f)
            return true;


        deltaMovement.x += isGoingRight ? -skinWidth : skinWidth;
        deltaMovement.y = Mathf.Abs(Mathf.Tan(angle * Mathf.Deg2Rad) * deltaMovement.x);
        ControlState.isMovingUpSlope = true;
        ControlState.isCollidingBelow = true;

        return true;
    }

    // Calculate the distance bewtween the horizontal and vertical rays
    public void CalculateRayBounds()
    {
        float colliderWidth = entityBoxCollider.size.x * Mathf.Abs(entityTransform.localScale.x) - (2 * skinWidth);
        horizontalDistanceBetweenRays = colliderWidth / (totalVerticalRays - 1);

        float colliderHeight = entityBoxCollider.size.y * Mathf.Abs(entityTransform.localScale.y) - (2 * skinWidth);
        verticalDistanceBetweenRays = colliderHeight / (totalHorizontalRays - 1);
    }


    public void OnTriggerEnter2D(Collider2D otherCol)
    {
        
    }

    public void OnTriggerExit2D(Collider2D otherCol)
    {
        
    }
}
