using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bounceable : MonoBehaviour
{
    [SerializeField] private float launchHeight;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        IBouncer target = collision.GetComponent<IBouncer>();

        if (target != null)
        {
            if(collision.transform.position.y > transform.position.y)
                target.Bounce(launchHeight);
        }
    }

}
