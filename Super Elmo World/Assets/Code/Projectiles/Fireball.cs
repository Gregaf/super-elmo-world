using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fireball : Projectile, ILaunchable
{
    public float fallSpeed;
    public float bounceHeight;

    private float currentXVelocity;

    protected override void Awake()
    {
        base.Awake();

    }

    private void Update()
    {
        velocity.y += fallSpeed * Time.deltaTime;
        velocity.x = Mathf.SmoothDamp(velocity.x, travelSpeed, ref currentXVelocity, bulletAcceleration);

        if (controller2D.collisionInfo.isGrounded)
            velocity.y = bounceHeight;

        controller2D.Move(velocity * Time.deltaTime);
    }

    public void Launch(float launchSpeed, int direction)
    {
        velocity.x = launchSpeed * direction;

        travelSpeed *= direction;
    }


    protected override void OnTriggerEnter2D(Collider2D collision)
    {
        base.OnTriggerEnter2D(collision);

    }
}
