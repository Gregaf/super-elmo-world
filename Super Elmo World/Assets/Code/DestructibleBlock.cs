using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestructibleBlock : Block
{
   public override void OnHitBlock()
   {
       base.OnHitBlock();
       

       Destroy(this.gameObject);
   }
   
}