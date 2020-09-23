using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class YAxisInverter : MonoBehaviour
{
    public LayerMask targetLayers;
    public float cooldownTime;

    private BoxCollider2D objectCollider2D;

    private void Awake()
    {
        objectCollider2D = this.GetComponent<BoxCollider2D>();

    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (objectCollider2D.IsTouchingLayers(targetLayers))
        {
            if (collision.GetComponent<Controller2D>() != null)
            {
                StartCoroutine(IgnoreCollisionsForTime(cooldownTime));

                Controller2D otherObj = collision.GetComponent<Controller2D>();

                
                otherObj.InvertYAxis();
            }
            else
            {
                Debug.LogError($"{collision.gameObject} does not have type of {typeof(Controller2D)}");
            }

        }
    }

    private IEnumerator IgnoreCollisionsForTime(float ignoreTime)
    {
        objectCollider2D.enabled = false;
        yield return new WaitForSeconds(ignoreTime);
        objectCollider2D.enabled = true;
    }

}
