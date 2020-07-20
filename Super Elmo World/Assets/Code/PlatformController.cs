using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class PlatformController : MonoBehaviour
{
    public enum PlatformType
    { 
        Looping,
        OneWay,
        Toggle
    }

    public PlatformType platformType;

    public MovingPlatform movingPlatform;
    public List<Transform> path = new List<Transform>();

    private int index = 0;

    private void Awake()
    {
        movingPlatform = GetComponentInChildren<MovingPlatform>();

        
    }

    // TODO: - Have a wait time once reaching a point
    //       - Cyclic option, so index 0 -> 1-> 2 -> 0...
    private void Update()
    {
        if (pathIsEmpty())
        {
            Debug.LogWarning("Platform Controller has no path's set, " + this.gameObject);
            return;
        }

        if (platformType == PlatformType.Looping)
        {

            // If the distance between the moving platform and the path point is close enough, then move to the next index.
            if (Vector2.Distance(movingPlatform.gameObject.transform.position, path[index].position) < movingPlatform.distanceToDestination)
            {
                // The end of the list is reached, so the list is reversed to traverse back through the path.
                if ((index + 1) >= path.Count)
                {
                    index = 0;
                    path.Reverse();
                }

                index++;
            }
            else
            {
                movingPlatform.MoveToPoint(path[index]);
            }

        
        }


    }

    // Only applies to one way platform.
    public void ResetMovingPlatform()
    {
        index = 0;
        movingPlatform.MoveToPoint(path[index]);
    }

    // Return true if the path list is empty.
    private bool pathIsEmpty()
    {
        return (path.Count == 0);
    }


    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        int listLength = path.Count;

        for (int i = 0; i < listLength; i++)
        {
            if (i == (listLength - 1))
                continue;

            Gizmos.DrawLine(path[i].position, path[i + 1].position);
        }

    }
}
