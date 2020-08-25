using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KillZone : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collider2D)
    {
        if (collider2D.GetComponent<IDamageable>() != null)
        {
            collider2D.GetComponent<IDamageable>().Kill();
        }
        else if (collider2D.GetComponent<IRespawnable>() != null)
        {
            collider2D.GetComponent<IRespawnable>().Respawn();
        }
    }
}
