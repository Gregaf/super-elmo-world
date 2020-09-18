using UnityEngine;

public class BindToCameraBounds : MonoBehaviour
{

    private Camera gameCamera;
    private CharacterController2D controller2D;
    private BoxCollider2D boxCollider;
    private Transform cameraPosition;

    private Vector3 cameraBounds;
    private Vector2 objectExtents;

    float left;
    float right;
    float down;
    float up;

    void Start()
    {
        controller2D = this.GetComponent<CharacterController2D>();
        gameCamera = FindObjectOfType<Camera>();
        boxCollider = this.GetComponent<BoxCollider2D>();
        cameraPosition = gameCamera.transform;

        objectExtents = new Vector2(boxCollider.bounds.extents.x, boxCollider.bounds.extents.y);
    }


    private void LateUpdate()
    {
        
        Vector3 boundedPosition = transform.position;
        Vector2 rightUp = gameCamera.ScreenToWorldPoint(new Vector2(Screen.width, Screen.height));
        Vector2 leftDown = gameCamera.ScreenToWorldPoint(Vector2.zero);

        // Review: Remove bottom bound so players can fall off screen.
        // Review: Should top bound be removed too?
        // Review: Rather than clamping should little force be applied, and then kill players that exceed the bounds past a point?
        //boundedPosition.x = Mathf.Clamp(boundedPosition.x, leftDown.x + objectExtents.x, rightUp.x - objectExtents.x);
        //boundedPosition.y = Mathf.Clamp(boundedPosition.y, leftDown.y + objectExtents.x, rightUp.y - objectExtents.y);
        if (transform.position.x < (leftDown.x + objectExtents.x))
        {
            controller2D.AddForce(new Vector2(.5f, 0f));
        }
        else if (transform.position.x > (rightUp.x - objectExtents.x))
        {
            controller2D.AddForce(new Vector2(-.5f, 0f));

        }


        transform.position = boundedPosition;
        
    }

}
