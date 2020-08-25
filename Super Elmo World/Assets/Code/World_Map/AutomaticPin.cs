using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace WorldMap
{
    public class AutomaticPin : Pin
    {
        public Pin fromTarget;
        public Pin toTarget;

        public void Start()
        {
        }

        private Pin ProperPath(Pin lastPinVisted)
        {
            if (lastPinVisted != fromTarget)          
                return this.fromTarget;     
            else
                return this.toTarget;
                
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.GetComponent<PlayerController>() != null)
            {
                PlayerController player = collision.GetComponent<PlayerController>();

                StartCoroutine(WaitUntilPlayerFinishes(player));
            }
        }

        IEnumerator WaitUntilPlayerFinishes(PlayerController player)
        {
            while (player.isMoving)
                yield return null;

            yield return new WaitForSeconds(0.25f);

            player.MoveTo(ProperPath(player.previousPin).transform.position);
        }

    }
}