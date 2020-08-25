using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterController2D))]
public class Fireball : Projectile, ILaunchable
{
    public float fallSpeed;
    public float bounceHeight;


    protected override void Awake()
    {
        base.Awake();

    }

    private void Update()
    {
        controller2D.SetVerticalForce(Mathf.Lerp(controller2D.Velocity.y, -fallSpeed, Time.deltaTime));
        controller2D.SetHorizontalForce(Mathf.Lerp(controller2D.Velocity.x, travelSpeed, Time.deltaTime * bulletAcceleration));

        if (controller2D.ControlState.isGrounded)
            controller2D.SetVerticalForce(bounceHeight);

    }

    public void Launch(float launchSpeed, int direction)
    {
        controller2D.SetHorizontalForce(launchSpeed * direction);

        travelSpeed *= direction;
    }


    protected override void OnTriggerEnter2D(Collider2D collision)
    {
        base.OnTriggerEnter2D(collision);

    }
}
