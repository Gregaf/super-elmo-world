using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestructibleBlock : Block
{
   public override void OnHitBlock()
   {
       print("fuck");
       base.OnHitBlock();
       //breaks block
       Destroy(this.gameObject);

   }
   
}