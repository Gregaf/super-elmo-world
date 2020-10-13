using System.Collections.Generic;
using UnityEngine;

public class CameraFollower : MonoBehaviour
{
    public bool adhearToBounds = true;

    [SerializeField] private BoxCollider2D levelBounds = null;
    [SerializeField] private float speed = 5;
    [SerializeField] private Vector3 offset = Vector3.zero;
    [SerializeField] private List<Transform> targetList = new List<Transform>();
    [SerializeField] private Transform lockTarget;
    private FSM cameraFsm;

    private Transform cameraTransform;
    private Vector3 vectorStore = Vector3.zero;

    // Cache the Main camera as it will be used often.
    public Camera GameCamera { get; private set; }
    public CameraFollowState CameraFState { get; private set; }
    public CameraLockState CameraLState { get; private set; }

    private void Awake()
    {
        GameCamera = this.GetComponent<Camera>();
        cameraTransform = this.transform;

        cameraFsm = new FSM();

        cameraFsm.isActive = true;
    }

    private void Start()
    {
        CameraFState = new CameraFollowState(this, targetList, speed, offset);
        CameraLState = new CameraLockState(this, lockTarget);

        cameraFsm.AddToStateList(0, CameraFState);
        cameraFsm.AddToStateList(1, CameraLState);

        cameraFsm.InitializeFSM(CameraFState);
    }

    private void LateUpdate()
    {
        cameraFsm.UpdateCurrentState();


    }

    

    public void MoveCamera(float speed, Vector3 offset, Vector3 targetPosition)
    {
        Vector3 target = targetPosition + offset;
        Vector3 cameraExtents = GetCameraExtents();


        if (levelBounds != null)
        {
            target.y = Mathf.Clamp(target.y, levelBounds.bounds.min.y + (cameraExtents.y), levelBounds.bounds.max.y - (cameraExtents.y));
            target.x = Mathf.Clamp(target.x, levelBounds.bounds.min.x + (cameraExtents.x), levelBounds.bounds.max.x - (cameraExtents.x));
        }

        target.z = -10;
       

        cameraTransform.position = Vector3.SmoothDamp(cameraTransform.position, new Vector3( target.x, target.y, target.z), ref vectorStore, speed);
    }

    private Vector2 GetCameraExtents()
    {
        Vector2 bounds = Vector2.zero;
        bounds.y = GameCamera.orthographicSize;
        bounds.x = (bounds.y) * GameCamera.aspect;


        return bounds;
    }

    // TODO: Reimplement Zoom but passing parameters to allow the states to utilize the function indpenedently. Ex: Lock node would zoom to specifc level, whereas follow cam is dynamic resizing.
    public void ZoomCamera(float targetZoom, float zoomSpeed)
    {
        GameCamera.orthographicSize = Mathf.Lerp(GameCamera.orthographicSize, targetZoom, Time.deltaTime * zoomSpeed);
    }

    /// <summary>
    /// Creates a bounding box around all the target transforms then returns the center point
    /// of the bounded area.
    /// </summary>
    /// <returns>A Vector3 position of the center of the bounding box.</returns>
    public Vector3 GetCenterPoint()
    {
        if (targetList.Count == 1)
            return targetList[0].position;

        Bounds newBounds = new Bounds(targetList[0].position, Vector3.zero);

        for (int i = 0; i < targetList.Count; i++)
        {
            newBounds.Encapsulate(targetList[i].position);
        }

        return newBounds.center;
    }

    public float GetGreatestDistance()
    {
        Bounds newBounds = new Bounds(targetList[0].position, Vector3.zero);

        for (int i = 0; i < targetList.Count; i++)
        {
            newBounds.Encapsulate(targetList[i].position);
        }

        return newBounds.size.x;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;

    }
}
