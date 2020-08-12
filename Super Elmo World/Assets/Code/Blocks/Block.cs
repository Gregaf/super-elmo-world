using System;
using System.Collections;
using UnityEngine;

public abstract class Block : MonoBehaviour
{
    // Public
    public GameObject contains;
    public bool activated = false;

    // Private 
    [SerializeField] private LayerMask detectMask = 0;
    [SerializeField] private float bounceHeight = .5f;
    [SerializeField] private float bounceSpeed = 6f;
    private float rayLength = 0.6f;

    // Protected
    protected Transform blockTransform;
    protected BoxCollider2D boxCollider;

    // Events
    public event EventHandler blockHitEventHandler;


    protected virtual void Awake()
    {
        boxCollider = GetComponent<BoxCollider2D>();
        blockTransform = this.transform;

    }

    protected virtual void Start()
    {
        
    }
    protected virtual void Update()
    {
        if (!activated && Physics2D.Raycast(transform.position, Vector2.down, rayLength, detectMask))
        {
            OnHitBlock();
        }

    }
    
    public virtual void OnHitBlock()
    {
        activated = true;
        blockHitEventHandler?.Invoke(this, EventArgs.Empty);
    }

    // Handles the effect of block moving up and down when being hit.
    // Review: Maybe move just the aesthetic portion of the block to avoid moving the collider?
    // Review: Also possibly have it bounce based on the direction the player hits from. This would require a parameter to be passed.
    protected IEnumerator BlockBounce()
    {
        Vector2 originalPosition = transform.position;
        Vector2 newPosition = new Vector2(transform.position.x, blockTransform.position.y + bounceHeight);
        while (true)
        {
            blockTransform.position = Vector2.MoveTowards(transform.position, newPosition, Time.deltaTime * bounceSpeed);

            if (blockTransform.position.y >= (originalPosition.y + bounceHeight))
                break;

            yield return null;
        }

        while (true)
        {
            blockTransform.position = Vector2.MoveTowards(transform.position, originalPosition, Time.deltaTime * bounceSpeed);

            if (blockTransform.position.y <= (originalPosition.y))
                break;

            yield return null;
        }
    }

}
