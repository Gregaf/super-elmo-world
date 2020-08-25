using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarryBlock : MonoBehaviour, ILiftable
{
    public float gravity = -30f;
    public float bounceHeight;
    public float maxNumberOfBounces;

    private float numberOfBounces;
    private CharacterController2D controller2D;

    private void Awake()
    {
        controller2D = this.GetComponent<CharacterController2D>();
        
    }

    private void Update()
    {
        if (numberOfBounces < maxNumberOfBounces && controller2D.ControlState.isGrounded)
        {
            controller2D.SetVerticalForce(bounceHeight / numberOfBounces);
            numberOfBounces++;
        }


        controller2D.SetHorizontalForce(Mathf.Lerp(controller2D.Velocity.x, 0, Time.deltaTime * 2f));
        controller2D.SetVerticalForce(Mathf.Lerp(controller2D.Velocity.y, gravity, Time.deltaTime));
    }

    public GameObject Lift()
    {
        gravity = 0;

        return this.gameObject;
    }

    public void Release(Vector2 releaseVector)
    {
        numberOfBounces = 1;
        gravity = -15f;
        controller2D.SetForce(releaseVector);
        
    }
}
