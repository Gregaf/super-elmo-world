using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MatchFloorOrient : MonoBehaviour
{
    [SerializeField] private LayerMask collisionMask;
    
    private Transform aestheticTransform;
    private Controller2D controller2D;
    private Vector2 direction;

    private void Awake()
    {
        aestheticTransform = this.transform;
        controller2D = this.GetComponentInParent<Controller2D>();
    }

    private void Start()
    {
        direction = controller2D.YAxisIsInverted ? Vector2.up : Vector2.down;
    }

    private void OnEnable()
    {
        controller2D.OnYAxisInverted += ChangeRayDirection;
    }

    private void OnDisable()
    {
        controller2D.OnYAxisInverted -= ChangeRayDirection;
    }

    private void Update()
    {
        RaycastHit2D hit2D = Physics2D.Raycast(aestheticTransform.position, direction, 2f, collisionMask);

        if (hit2D)
        {
            

            float angle = Vector2.Angle(hit2D.normal, Vector2.up);

            Vector3 newRotation = Vector3.zero;

            newRotation.z = angle;

            aestheticTransform.localEulerAngles = newRotation;
        }
        

    }

    private void ChangeRayDirection() => direction = -direction;

}
