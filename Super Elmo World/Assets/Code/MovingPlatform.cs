using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    public float speed;
    public float distanceToDestination;

    private Vector2 temp;


    public void MoveToPoint(Transform point)
    {
        transform.position = Vector2.SmoothDamp(transform.position, point.position, ref temp, speed);

    }

}
