using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArcedRock : Projectile
{
    [SerializeField] private float maxHeight;
    [SerializeField] private float minHeight;
    [SerializeField] private float fallSpeed;

    public Transform target;
    private Vector3 intialPos;

    protected override void Awake()
    {
        base.Awake();
    }

    private void Start()
    {
        intialPos = transform.position;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            transform.position = intialPos;
            Launch(target.position);
        }

        
    }

    protected override void OnTriggerEnter2D(Collider2D collision)
    {
        base.OnTriggerEnter2D(collision);
    }

    private void CalculateHeight()
    { 
        
    }

    public override void Launch(Vector2 targetPosition)
    {
        float deltaY = targetPosition.y - projectileTransform.position.y;
        float deltaX = targetPosition.x - projectileTransform.position.x;

        float height = minHeight;


        height = Mathf.Lerp(height, maxHeight, deltaY / maxHeight);
        

        velocity.y = Mathf.Sqrt(-2 * fallSpeed * height);
        velocity.x = deltaX / (Mathf.Sqrt(-2 * height / fallSpeed) + Mathf.Sqrt(2 * (Mathf.Abs((deltaY - height)) * -1f) / fallSpeed));

        StartCoroutine(ArcToPoint(targetPosition));
    }

    IEnumerator ArcToPoint(Vector2 targetPosition)
    {
        while (Vector2.Distance(projectileTransform.position, targetPosition) > 0.15f)
        {
            velocity.y += fallSpeed * Time.deltaTime;



            controller2D.Move(velocity * Time.deltaTime);

            yield return null;
        }

        velocity = Vector2.zero;
        yield return null;
    }
}
