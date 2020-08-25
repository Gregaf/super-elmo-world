using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace WorldMap
{
    public class PlayerController : MonoBehaviour
    {
        public Level currentLevel;
        public Pin currentPin;
        public Pin previousPin;
        public float playerMoveSpeed;

        public PlayerInputHandler PlayerInput { get; private set; }
        public bool isMoving { get; private set; }

        private void Awake()
        {
            PlayerInput = this.GetComponent<PlayerInputHandler>();
        }

        private void Start()
        {
            PlayerInput.playerControls.World.Select.performed += Select;

            PlayerInput.playerControls.World.PickDirection.performed += MoveTo;
        }

        private void OnEnable()
        {

        }

        private void OnDisable()
        {
            PlayerInput.playerControls.World.Select.performed -= Select;

            PlayerInput.playerControls.World.PickDirection.performed -= MoveTo;
        }


        private void Select(InputAction.CallbackContext context)
        {
            if (isMoving || currentLevel == null)
                return;

            currentLevel.PlayLevel();
        }

        private void MoveTo(InputAction.CallbackContext context)
        {
            if (isMoving || currentLevel == null)
                return;

            isMoving = true;
            Vector2 direction = context.ReadValue<Vector2>();

            Pin target = isValidDirection(direction);

            if (target != null)
            {
                StartCoroutine(MoveToLocation(target.transform.position));
            }
            else
            {
                isMoving = false;

                Debug.Log($"No level with the direction {direction}.");
            }
        }

        public void MoveTo(Vector2 targetPosition)
        {
            isMoving = true;
            //StopAllCoroutines();
            StartCoroutine(MoveToLocation(targetPosition));
        }


        private Pin isValidDirection(Vector2 givenDirection) 
        {
            if (givenDirection == Vector2.up)
            {
                return (currentLevel.levelPaths[0]);
            }
            else if (givenDirection == Vector2.down)
            {
                return (currentLevel.levelPaths[1]);
            }
            else if (givenDirection == Vector2.right)
            {
                return (currentLevel.levelPaths[2]);
            }
            else if (givenDirection == Vector2.left)
            {
                return (currentLevel.levelPaths[3]);
            }
   
            return null;
        }

        private IEnumerator MoveToLocation(Vector2 targetPosition)
        {

            while (Vector2.Distance(this.transform.position, targetPosition) > 0.1f)
            {
                this.transform.position = Vector2.MoveTowards(this.transform.position, targetPosition, Time.deltaTime * playerMoveSpeed);
                
                yield return null;
            }

            isMoving = false;

            yield return null;
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.GetComponent<Pin>() != null)
            {
                previousPin = currentPin;
                currentPin = collision.GetComponent<Pin>();

                if (currentPin.GetComponent<Level>() != null)
                {
                    currentLevel = collision.GetComponent<Level>();
                }
                else
                    currentLevel = null;
            }

        }

    }





}
