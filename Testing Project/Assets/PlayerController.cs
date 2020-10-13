using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed;
    public float jumpPower;
    public LayerMask floor;
    
    private Rigidbody2D rb2D;

    public bool canJump;
    public bool Jumped;

    Vector2 input;

    private void Start()
    {
        rb2D = this.GetComponent<Rigidbody2D>();

    }

    private void Update()
    {
        input = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));

        Jumped = false;

        if(Input.GetKeyDown(KeyCode.Space))
        {
            Jumped = true;
        }

        canJump = Physics2D.Raycast(transform.position, Vector2.down, 1.15f, floor);

    }

    private void FixedUpdate()
    {
        if (canJump && Jumped)
        {
            rb2D.velocity = new Vector2(rb2D.velocity.x, jumpPower);
        }

        rb2D.velocity = new Vector2(speed * input.x, rb2D.velocity.y);


    }





}
