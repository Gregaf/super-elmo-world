using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SMTest
{
    public class PlayerGrowth : MonoBehaviour
    {
        [Header("Animator Overrides")]
        [SerializeField] private AnimatorOverrideController smallAnimations;
        [SerializeField] private AnimatorOverrideController bigAnimations;


        private PlayerController playerController;
        private FSM growthFsm;

        public event Action OnShrink;

        private void Awake()
        {
            playerController = this.GetComponent<PlayerController>();
            growthFsm = new FSM();

        }

        private void Start()
        {
            growthFsm.AddToStateList((int) PlayerGrowthStates.SMALL, new Small(playerController, smallAnimations, this));
            growthFsm.AddToStateList((int)PlayerGrowthStates.BIG, new Big(playerController, bigAnimations, this));

            growthFsm.InitializeFSM((int) PlayerGrowthStates.SMALL);
        }


        public void GrowTo(PlayerGrowthStates growthState)
        {
            if (growthFsm.IsCurrentState((int) growthState))
            {
                // ALready currently in that state...
                // Maybe add some points
                return;
            }

            growthFsm.ChangeCurrentState((int) growthState);
        }

        public void Shrink()
        {
            OnShrink?.Invoke();
        }

        private void OnTriggerEnter2D(Collider2D collider2D)
        { 
            
        }

    }
}