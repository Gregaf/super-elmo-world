using System.Collections.Generic;
using UnityEngine;

public class CameraFollower : MonoBehaviour
{
    public bool adhearToBounds = true;

    [SerializeField] private BoxCollider2D levelBounds = null;
    [SerializeField] private float speed = 5;
    private Transform[] targets;
    private FSM cameraFsm;

    // Cache the Main camera as it will be used often.
    public Camera GameCamera { get; private set; }

    private void Awake()
    {
        GameCamera = this.GetComponent<Camera>();

        cameraFsm = new FSM();

        int length = targets.Length;
        Transform[] targetPositions = new Transform[length];

        Debug.Log(targetPositions.Length);

        for (int i = 0; i < length; i++)
        {
            targetPositions[i] = targets[i].transform;
        }


        cameraFsm.isActive = true;
    }

    private void LateUpdate()
    {
        cameraFsm.UpdateCurrentState();


    }


    private Vector2 GetCameraExtents()
    {
        Vector2 bounds = Vector2.zero;
        bounds.y = GameCamera.orthographicSize;
        bounds.x = (bounds.y) * GameCamera.aspect;


        return bounds;
    }

    // TODO: Reimplement Zoom but passing parameters to allow the states to utilize the function indpenedently. Ex: Lock node would zoom to specifc level, whereas follow cam is dynamic resizing.
    //private void ZoomCamera()
    //{
    //    float newZoom = Mathf.Lerp(minimumZoomDistance, maximumZoomDistance, GetGreatestDistance() / 50);

    //    gameCamera.orthographicSize = Mathf.Lerp(gameCamera.orthographicSize, newZoom, Time.deltaTime * 3);
    //}

    /// <summary>
    /// Creates a bounding box around all the target transforms then returns the center point
    /// of the bounded area.
    /// </summary>
    /// <returns>A Vector3 position of the center of the bounding box.</returns>
    public Vector3 GetCenterPoint()
    {
        if (targets.Length == 1)
            return targets[0].position;

        Bounds newBounds = new Bounds(targets[0].position, Vector3.zero);

        for (int i = 0; i < targets.Length; i++)
        {
            newBounds.Encapsulate(targets[i].position);
        }

        return newBounds.center;
    }

    public float GetGreatestDistance()
    {
        Bounds newBounds = new Bounds(targets[0].position, Vector3.zero);

        for (int i = 0; i < targets.Length; i++)
        {
            newBounds.Encapsulate(targets[i].position);
        }

        return newBounds.size.x;
    }
}
