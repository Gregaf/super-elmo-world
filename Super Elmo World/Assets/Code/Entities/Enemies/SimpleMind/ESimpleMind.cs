using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SMTest {
    public enum ESimpleStates
    {
        PATROL,
        DEAD
    }

    public class ESimpleMind : Enemy
    {
        protected override void Awake()
        {
            base.Awake();
        }
        protected override void Start()
        {

            // Add States here.
            EnemyFsm.AddToStateList((int)ESimpleStates.PATROL, new ESimplePatrol());
    
    }

        protected override void Die()
        {
            base.Die();
        }

        protected override void OnTriggerEnter2D(Collider2D collider2D)
        {
            base.OnTriggerEnter2D(collider2D);

        }

    }
}
