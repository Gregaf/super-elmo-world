using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Block : MonoBehaviour
{
    public GameObject contains;
    public LayerMask detectMask;
    public float rayLength = 0.6f;
    public bool activated = false;
    public virtual void OnHitBlock()
    {
        activated = true;
    }
    public void Update()
   {
       Debug.DrawRay(this.transform.position,Vector2.down * rayLength, Color.green);
      if( Physics2D.Raycast(this.transform.position, Vector2.down,rayLength, detectMask) && !activated)
      {
          OnHitBlock();
      }
       
   }
}
