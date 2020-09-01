using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace SMTest
{
    [RequireComponent(typeof(CharacterController2D), typeof(PlayerInputHandler))]
    public class PlayerController : Entity
    {



        public PlayerMoveProperties playerMoveProperties;
        public LayerMask WhatIsWallJumpable;
        public float inviciblityTime;

        public PlayerInputHandler PlayerInputHandler { get; private set; }
        public CharacterController2D Controller2D { get; private set; }
        public Animator animator { get; private set; }
        public FSM movementModeFsm { get; private set; }

        private PlayerGrowth playerGrowth;

        public bool isCrouching;
        private Vector2 crouchSize;
        private Vector2 originalSize;

        private Invincibility invincibility;

        protected override void Awake()
        {
            base.Awake();

            PlayerInputHandler = this.GetComponent<PlayerInputHandler>();
            Controller2D = this.GetComponent<CharacterController2D>();
            invincibility = this.GetComponent<Invincibility>();
            movementModeFsm = new FSM();
            animator = this.GetComponentInChildren<Animator>();
        }

        protected override void Start()
        {
            base.Start();

            movementModeFsm.AddToStateList((int) PlayerMoveModes.GROUND, new GroundMovement(this));

            movementModeFsm.InitializeFSM((int) PlayerMoveModes.GROUND);

            playerMoveProperties.baseGravity = (playerMoveProperties.jumpHeight) / (2 * (playerMoveProperties.jumpTime * playerMoveProperties.jumpTime));

            playerMoveProperties.jumpVelocity = Mathf.Sqrt(2 * playerMoveProperties.jumpHeight * playerMoveProperties.baseGravity);

            crouchSize = new Vector2(1, 0.9f);

            PlayerInputHandler.playerControls.Basic.ChangeDirection.performed += Flip;
        }

        private void OnEnable()
        {
        }

        private void OnDisable()
        {
            PlayerInputHandler.playerControls.Basic.ChangeDirection.performed -= Flip;
        }

        private void Update()
        {
            movementModeFsm.UpdateCurrentState();


        }

        private void LateUpdate()
        {
            SetAnimatorParameters();
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {

        }

        private void OnTriggerExit2D(Collider2D collision)
        {

        }

        private void Flip(InputAction.CallbackContext context)
        {
            if (context.ReadValue<float>() < 0 && isFacingRight || context.ReadValue<float>() > 0 && !isFacingRight)
            {
                isFacingRight = !isFacingRight;
                Vector3 localScale = transform.localScale;
                localScale.x *= -1;

                this.transform.localScale = localScale;
            }
        }

        private void SetAnimatorParameters()
        {
            animator.SetBool("isGrounded", Controller2D.ControlState.isGrounded);
            animator.SetBool("isCrouching", isCrouching);
            animator.SetFloat("xMove", Mathf.Abs(Controller2D.Velocity.x));
            animator.SetFloat("yMove", Controller2D.Velocity.y);
            animator.SetBool("invincible", invincibility.isInvincible);


        }
        public bool TouchingWall(int direction)
        {
            if (direction == 0)
                return false;

            float xPoint = direction == 1 ? this.entityCollider.bounds.max.x : this.entityCollider.bounds.min.x;
            Vector2 wallpoint = new Vector2(xPoint + (0.02f * direction), this.entityCollider.bounds.center.y);

            return Physics2D.OverlapPoint(wallpoint, WhatIsWallJumpable);
        }

        public void Crouch()
        {
            StartCoroutine(CrouchAction());
        }

        private IEnumerator CrouchAction()
        {
            isCrouching = true;
            originalSize = entityCollider.size;
            controller2D.ModifySize(crouchSize);

            while (!(CanStand() && PlayerInputHandler.MovementInput.y != -1))
            {
                

                yield return null;
            }
            Debug.Log("Wtf");
            isCrouching = false;
            controller2D.ModifySize(originalSize);

        }
        private bool CanStand()
        {
            float yPoint = Mathf.Abs(crouchSize.y - originalSize.y);
            Vector2 standPoint = new Vector2(this.entityCollider.bounds.center.x, this.entityCollider.bounds.max.y + yPoint);

            Debug.DrawLine(this.transform.position, standPoint);

            return !Physics2D.OverlapPoint(standPoint, controller2D.platformMask);
        }
    }
}