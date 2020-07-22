using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Block : MonoBehaviour
{
    public GameObject contains;
    public LayerMask detectMask;
    public float rayLength = 0.6f;
    public bool activated = false;

    public delegate void OnHitBlockCallback();
    public static event OnHitBlockCallback onHitBlock;

    private BoxCollider2D boxCollider;
    private Vector2 rayOrigin;
    private float distancebetweenRays;
    private const float skinWidth = 0.005f;

    protected void Awake()
    {
        boxCollider = GetComponent<BoxCollider2D>();

    }

    protected void Start()
    {
        Vector2 blockLocalScale = transform.localScale;
        Vector2 size = new Vector2(boxCollider.size.x * Mathf.Abs(blockLocalScale.x),
                           boxCollider.size.y * Mathf.Abs(blockLocalScale.y)) / 2;
        Vector2 center = new Vector2(boxCollider.offset.x * blockLocalScale.x, boxCollider.offset.y * blockLocalScale.y);

        rayOrigin = (Vector2)this.transform.position + new Vector2(center.x - size.x + skinWidth, center.y - size.y + skinWidth);
        distancebetweenRays = CalculateRaySpacing();
    }

    public virtual void OnHitBlock()
    {
        activated = true;
        onHitBlock.Invoke();

    }
    public void Update()
   {
        for (int i = 0; i < 4; i++)
        {
            Vector2 rayVector = new Vector2(rayOrigin.x + (i * distancebetweenRays), rayOrigin.y);
            Debug.DrawRay(rayVector, Vector2.down * rayLength, Color.yellow);

            RaycastHit2D rayCastHit = Physics2D.Raycast(rayVector, Vector2.down, rayLength, detectMask);

            if (rayCastHit && !activated)
            {
                OnHitBlock();
            }
        }
   }

    private float CalculateRaySpacing()
    {
        float colliderHeight = boxCollider.size.y * Mathf.Abs(transform.localScale.y) - (2 * skinWidth);

        return (colliderHeight / (4 - 1));
    }
}
