using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider2D), typeof(Rigidbody2D))]
public abstract class RayCastController : MonoBehaviour
{
    public int horizontalRayCount = 4, verticalRayCount = 4;

    protected float horizontalRaySpacing, verticalRaySpacing;

    protected RaycastOrigins raycastOrigins;
    protected BoxCollider2D objectCollider;
    protected Rigidbody2D rb2d;
    protected Transform objectTransform;


    protected const float SKIN_WIDTH = 0.015f;

    protected virtual void Awake()
    {
        objectCollider = this.GetComponent<BoxCollider2D>();
        rb2d = this.GetComponent<Rigidbody2D>();
        objectTransform = this.GetComponent<Transform>();

    }

    protected virtual void Start()
    {
        rb2d.isKinematic = true;

        CalculateRaySpacing();
    }

    protected struct RaycastOrigins
    {
        public Vector2 topLeft, topRight;
        public Vector2 bottomLeft, bottomRight;
    }

    /// <summary>
    /// Bounds the amount of possible rays, and then calculates the space between each ray.
    /// </summary>
    protected void CalculateRaySpacing()
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
    protected void UpdateRaycastOrigins()
    {
        Bounds bounds = objectCollider.bounds;
        bounds.Expand(SKIN_WIDTH * -2);

        raycastOrigins.topLeft = new Vector2(bounds.min.x, bounds.max.y);
        raycastOrigins.topRight = bounds.max;
        raycastOrigins.bottomLeft = bounds.min;
        raycastOrigins.bottomRight = new Vector2(bounds.max.x, bounds.min.y);
    }
}
