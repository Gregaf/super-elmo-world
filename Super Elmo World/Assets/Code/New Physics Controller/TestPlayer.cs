using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestPlayer : MonoBehaviour
{
    public enum SmoothMode
    { 
        lerp,
        smoothDamp
    }

    public SmoothMode currentMode;

    public float walkSpeed;
    public float runSpeed;
    public float currentMoveSpeed;
    public float knockback;

    public float jumpHeight;
    public float minimumJumpHeight;
    public float jumpTime;
    public float gravityScale;
    public float accelerationTime;
    public float runAccelerationRate;

    private float jumpVelocity;
    private float minimumJumpVelocity;
    private float minimumJumpTime;
    private float gravity;
    private float minJumpTimer;

    public float graceTimer; //  Subscribe to fell event on enter for fall?

    public Vector2 velocity;
    Controller2D controller;


    private float store;

    void Awake()
    {
        controller = this.GetComponent<Controller2D>();
        
    }

    private void Start()
    {
        jumpVelocity = (2 * jumpHeight) / jumpTime;
        gravity = -jumpVelocity / jumpTime;
        minimumJumpVelocity = Mathf.Sqrt((jumpVelocity * jumpVelocity) + 2 * gravity * (jumpHeight - minimumJumpHeight));
        minimumJumpTime = jumpTime - (2 * (jumpHeight - minimumJumpHeight) / (jumpVelocity + minimumJumpVelocity));
    }

    private void OnEnable()
    {
        controller.OnFellEvent += JumpGracePeriod;
        controller.OnTriggerEnter += PlayerBump;
        controller.OnYAxisInverted += InvertBounce;
    }

    private void OnDisable()
    {
        controller.OnFellEvent -= JumpGracePeriod;
        controller.OnTriggerEnter -= PlayerBump;
        controller.OnYAxisInverted -= InvertBounce;
    }

    private void PlayerBump(Collider2D otherCol)
    {
        if (otherCol.GetComponent<TestPlayer>() != null)
        {
            TestPlayer otherPlayer = otherCol.GetComponent<TestPlayer>();

            float direction = Mathf.Sign(transform.position.x - otherPlayer.transform.position.x);

            velocity.x = (direction * knockback);
        }
    }

    // Update is called once per frame
    void Update()
    {
        float xInput = Input.GetAxisRaw("Horizontal");

        if (controller.collisionInfo.below || controller.collisionInfo.above)
            velocity.y = 0;

        
        // Handle skidding, changing directions when moving fast.
        if (Mathf.Abs(velocity.x) > 2 && Mathf.Sign(velocity.x) != xInput && controller.collisionInfo.below)
        {
            currentMoveSpeed = 0;
            print("Skidding!");
        }

        float dynamicGravity = 0;

        if(controller.collisionInfo.isGrounded)
            currentMoveSpeed = AdjustMoveSpeed(currentMoveSpeed);

        if (velocity.y < 0)
        {
            dynamicGravity = gravity * gravityScale;
        }
        else
            dynamicGravity = gravity;




        velocity.x = Mathf.SmoothDamp(velocity.x, xInput * currentMoveSpeed, ref store, accelerationTime);
        
        if (Input.GetButtonDown("Jump"))
        {
            if (controller.collisionInfo.isGrounded || graceTimer > 0)
            {
                minJumpTimer = minimumJumpTime;
                graceTimer = 0;
                velocity.y = jumpVelocity;
            }

        }
        
        if (Input.GetButtonUp("Jump") && minJumpTimer > 0)
        {
            velocity.y = minimumJumpVelocity;
        }

        minJumpTimer -= Time.deltaTime;
        graceTimer -= Time.deltaTime;

        velocity.y += dynamicGravity * Time.deltaTime;

        controller.Move((velocity * Time.deltaTime));
    }


    private void InvertBounce()
    {
        velocity = new Vector2(velocity.x, minimumJumpVelocity);
    }

    public void JumpGracePeriod()
    {
        graceTimer = 0.1f;
        print("wtf");
    }

    float AdjustMoveSpeed(float newSpeed)
    {
        if (Input.GetKey(KeyCode.X))
        {
            newSpeed += Time.deltaTime * runAccelerationRate;
        }
        else
        {
            newSpeed -= Time.deltaTime * runAccelerationRate;
        }

        newSpeed = Mathf.Clamp(newSpeed, walkSpeed, runSpeed);

        return newSpeed;
    }
}
