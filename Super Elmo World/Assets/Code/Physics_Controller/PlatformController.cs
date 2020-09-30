using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformController : RayCastController
{
    [SerializeField] private LayerMask passengerMask;

    [SerializeField] private Vector2 moveDistance;

    private List<PassengerMovement> passengers = new List<PassengerMovement>();
    private HashSet<Transform> movedPassengers = new HashSet<Transform>();
    private Dictionary<Transform, Controller2D> passengerDictionary = new Dictionary<Transform, Controller2D>();

    private struct PassengerMovement
    {
        public Transform transform;
        public Vector2 velocity;
        public bool standingOnPlatform;
        public bool movePassengerFirst;

        public PassengerMovement(Transform transform, Vector2 velocity, bool standingOnPlatform, bool movePassengerFirst)
        {
            this.transform = transform;
            this.velocity = velocity;
            this.standingOnPlatform = standingOnPlatform;
            this.movePassengerFirst = movePassengerFirst;
        }
    }

    protected override void Awake()
    {
        base.Awake();

    }

    protected override void Start()
    {
        base.Start();

    }

    private void Update()
    {
        UpdateRaycastOrigins();

        // Velocity = distance traveled multiplied by time.
        Vector2 velocity = moveDistance * Time.deltaTime;

        CalculatePassengerMove(velocity);

        MovePassengers(true);

        objectTransform.Translate(velocity, Space.World);

        MovePassengers(false);
    }

    private void MovePassengers(bool movePassengerFirst)
    {
        for (int i = 0; i < passengers.Count; i++)
        {
            if (!passengerDictionary.ContainsKey(passengers[i].transform))         
                passengerDictionary.Add(passengers[i].transform, passengers[i].transform.GetComponent<Controller2D>());
            
            if (passengers[i].movePassengerFirst == movePassengerFirst)         
                passengerDictionary[passengers[i].transform].Move(passengers[i].velocity, passengers[i].standingOnPlatform);
            
        }
    }

    private void CalculatePassengerMove(Vector2 velocity)
    {
        float currentXDirection = Mathf.Sign(velocity.x);
        float currentYDirection = Mathf.Sign(velocity.y);

        passengers.Clear();

        // Vertical movement
        if (velocity.y != 0)
        {
            float rayLength = Mathf.Abs(velocity.y) + SKIN_WIDTH; 

            for (int i = 0; i < verticalRayCount; i++)
            {
                Vector2 rayOrigin = (currentYDirection == -1) ? raycastOrigins.bottomLeft : raycastOrigins.topLeft;
                rayOrigin += (Vector2.right * (verticalRaySpacing * i));

                RaycastHit2D hit2D = Physics2D.Raycast(rayOrigin, Vector2.up * currentYDirection, rayLength, passengerMask);

                Debug.DrawRay(rayOrigin, Vector2.up * currentYDirection * rayLength, Color.red);

                if (hit2D)
                {
                    if (!movedPassengers.Contains(hit2D.transform))
                    {
                        movedPassengers.Add(hit2D.transform);
                        float pushX = (currentYDirection == 1) ? velocity.x : 0;
                        float pushY = velocity.y - (hit2D.distance - SKIN_WIDTH) * currentYDirection;


                        passengers.Add(new PassengerMovement(hit2D.transform, new Vector2(pushX, pushY), currentYDirection == 1, true));
                    }

                }
            }
        }

        // Horizontal movement
        if (velocity.x != 0)
        {
            float rayLength = Mathf.Abs(velocity.x) + SKIN_WIDTH;

            for (int i = 0; i < horizontalRayCount; i++)
            {
                Vector2 rayOrigin = (currentXDirection == -1) ? raycastOrigins.bottomLeft : raycastOrigins.bottomRight;
                rayOrigin += (Vector2.up * (horizontalRaySpacing * i));

                RaycastHit2D hit2D = Physics2D.Raycast(rayOrigin, Vector2.right * currentXDirection, rayLength, passengerMask);

                Debug.DrawRay(rayOrigin, Vector2.right * currentXDirection * rayLength, Color.red);

                if (hit2D)
                {
                    if (!movedPassengers.Contains(hit2D.transform))
                    {
                        movedPassengers.Add(hit2D.transform);
                        float pushX = velocity.x - (hit2D.distance - SKIN_WIDTH) * currentXDirection;
                        float pushY = -SKIN_WIDTH;

                        passengers.Add(new PassengerMovement(hit2D.transform, new Vector2(pushX, pushY), false, true));
                    }
                }

            }
        }

            if (currentYDirection == -1 || (velocity.y == 0 && velocity.x != 0))
            {
                float rayLength = SKIN_WIDTH * 2f;

                for (int i = 0; i < verticalRayCount; i++)
                {
                    Vector2 rayOrigin = raycastOrigins.topLeft + (Vector2.right * (verticalRaySpacing * i));
                    //Vector2 rayOrigin = (currentYDirection == -1) ? raycastOrigins.bottomLeft : raycastOrigins.topLeft;
                    // rayOrigin += (Vector2.right * (verticalRaySpacing * i));

                    RaycastHit2D hit2D = Physics2D.Raycast(rayOrigin, Vector2.up, rayLength, passengerMask);

                    Debug.DrawRay(rayOrigin, Vector2.up * currentYDirection * rayLength, Color.red);

                    if (hit2D)
                    {
                        if (!movedPassengers.Contains(hit2D.transform))
                        {
                            movedPassengers.Add(hit2D.transform);
                            float pushX = velocity.x;
                            float pushY = velocity.y;

                            passengers.Add(new PassengerMovement(hit2D.transform, new Vector2(pushX, pushY), true, false));

                        }

                }
                }
            }

            movedPassengers.Clear();
    }
}
