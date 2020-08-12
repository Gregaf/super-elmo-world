using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MushroomPowerUp : PickUp
{
    [SerializeField] private float itemMoveSpeed = 5f;
    private float moveDirection;
    private float gravity = -15;
    private CharacterController2D controller2D;

    private PlayerBrain[] players;

    private void Awake()
    {
        controller2D = GetComponent<CharacterController2D>();

        players = FindObjectsOfType<PlayerBrain>();
    }

    protected override void Start()
    {
        base.Start();

        //moveDirection = GetClosestPlayer().isFacingRight ? 1 : -1;

        pickUpEventArgs = new PickUpEventArgs();


    }

    protected override void AttractionState()
    {
        controller2D.SetHorizontalForce(Mathf.Lerp(controller2D.Velocity.x, itemMoveSpeed * moveDirection, Time.deltaTime * 10));

        controller2D.SetVerticalForce(Mathf.Lerp(controller2D.Velocity.y, gravity, Time.deltaTime));

        if ((controller2D.ControlState.isCollidingLeft && moveDirection == -1) || (controller2D.ControlState.isCollidingRight && moveDirection == 1))
        {
            moveDirection *= -1;
        }

    }
    protected override void PayLoadState()
    {
    }

    protected override void ExpirationState()
    {
    }

    protected override void Update()
    {
        base.Update();
    }

    protected override void OnTriggerEnter2D(Collider2D collider2D)
    {
        if (collider2D.GetComponent<PlayerBrain>() != null)
        {
            pickUpEventHandler?.Invoke(this, pickUpEventArgs);
            Destroy(gameObject);
        }


    }

    private PlayerBrain GetClosestPlayer()
    {
        if (players.Length <= 0)
            return null;

        Array.Sort(players, (x, y) => x.transform.position.x.CompareTo(y.transform.position.x));

        return players[0];
    }


}
