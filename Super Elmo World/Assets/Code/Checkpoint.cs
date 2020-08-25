using System;
using UnityEngine;

public class Checkpoint : MonoBehaviour
{

    [SerializeField] private BoxCollider2D detectionCollider;
    // Will only be activated once to prevent activating previous checkpoints.
    private bool activated;


    public event EventHandler checkpointActivatedHandler;

    private void Awake()
    {
        detectionCollider = this.GetComponent<BoxCollider2D>();
    }


    private void OnTriggerEnter2D(Collider2D collider2D)
    {
    
        

    }

    private void OnDrawGizmosSelected()
    {
        Vector3 detectionVolume = Vector2.zero;
        Vector3 startPosition = Vector2.zero;

        if (detectionCollider != null)
        {

            detectionVolume = detectionCollider.size;
            startPosition = new Vector2(transform.position.x + detectionCollider.offset.x, transform.position.y + detectionCollider.offset.y);
        }

        Gizmos.color = Color.red;
        Gizmos.DrawCube(startPosition, detectionVolume);

    }

}
