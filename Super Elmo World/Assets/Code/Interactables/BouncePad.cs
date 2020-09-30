using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BouncePad : MonoBehaviour
{
    public float launchHeight;


    private void OnTriggerEnter2D(Collider2D collider2D)
    {
        if (collider2D.GetComponent<IBounceable>() != null)
        {
            collider2D.GetComponent<IBounceable>().Bounce(launchHeight);
        }
    }

}
