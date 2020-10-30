//#undef DEBUG

using System;
using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections.Generic;


[RequireComponent(typeof(Controller2D), typeof(PlayerInputHandler))]
public class PlayerController : Entity, IDamageable, IBouncer
{
    public AudioClip jumpSfx;

    public PlayerMoveProperties playerMoveProperties;
    public LayerMask WhatIsWallJumpable;
    public LayerMask WhatIsBackWall;
    public float inviciblityTime;

    //private SpriteRenderer spriteRenderer;
    private Invincibility invincibility;

    [Header("Animator Overrides")]
    [SerializeField] private AnimatorOverrideController smallAnimations;
    [SerializeField] private AnimatorOverrideController bigAnimations;

    public PlayerInputHandler PlayerInputHandler { get; private set; }
    public Animator animator { get; private set; }
    //public FSM movementModeFsm { get; private set; }
    public FSM baseMovementFSM { get; private set; }
    public FSM growthFsm { get; private set; }
    public HealthHandler Health_Handler { get; private set; }
    public PlayerData Player_Data { get; private set; }

    #region PlayerBaseStates
    public Idle IdleState { get; private set; }
    public Move MoveState { get; private set; }
    public Jump JumpState { get; private set; }
    public Fall FallState { get; private set; }
    public WallSliding WallSlideState { get; private set; }
    public Crouch CrouchState { get; private set; }
    public GroundPound GroundPoundState { get; private set; }
    public Dead DeadState { get; private set; }
    public Bubble BubbleState { get; private set; }
    #endregion

#if (DEBUG)
    private Rect windowRect = new Rect(30, 30, 120, 120);
    private void OnGUI()
    {
        windowRect = GUI.Window(0, windowRect, DebugHealthDisplay, "Health Display");
    }

    private void DebugHealthDisplay(int windowID)
    {
        string currentHealth = $"Health: {Health_Handler.health}";
        GUI.Label(new Rect(25, 25, 120, 20), currentHealth);
    }
#endif

    protected override void Awake()
    {
        base.Awake();

        Health_Handler = new HealthHandler();

        Player_Data = this.GetComponent<PlayerData>();
        PlayerInputHandler = this.GetComponent<PlayerInputHandler>();
        invincibility = this.GetComponent<Invincibility>();
        
        animator = this.GetComponentInChildren<Animator>();

        baseMovementFSM = new FSM();
        growthFsm = new FSM();
    }

    protected override void Start()
    {
        base.Start();

        IdleState = new Idle(this);
        MoveState = new Move(this);
        JumpState = new Jump(this);
        FallState = new Fall(this);
        WallSlideState = new WallSliding(this);
        CrouchState = new Crouch(this);
        GroundPoundState = new GroundPound(this);
        DeadState = new Dead(this);
        BubbleState = new Bubble(this);

        growthFsm.AddToStateList((int)PlayerGrowthStates.SMALL, new Small(this, 1, smallAnimations));
        growthFsm.AddToStateList((int)PlayerGrowthStates.BIG, new Big(this, 2, bigAnimations));
        growthFsm.AddToStateList((int)PlayerGrowthStates.DEAD, DeadState);
        growthFsm.AddToStateList((int)PlayerGrowthStates.BUBBLE, new Bubble(this));

        growthFsm.InitializeFSM((int)PlayerGrowthStates.BIG);
        baseMovementFSM.InitializeFSM(IdleState);
        

        playerMoveProperties.CalculatePhysicsValues();
    }

    private void OnEnable()
    {
        Control2D.OnTriggerEnter += BounceOnPlayer;
    }

    private void OnDisable()
    {
        Control2D.OnTriggerEnter -= BounceOnPlayer;
    }

    private void Update()
    {
#if (DEBUG)
        if (Input.GetKeyDown(KeyCode.G))
        {
            Health_Handler.LoseHealth(1);
        }

        Debug.Log($"Health: {Health_Handler.health}");
#endif

        

        // Stop y movement when hitting head.
        if ((Control2D.collisionInfo.below && velocity.y < 0 && !Control2D.collisionInfo.isOnSlope) || Control2D.collisionInfo.above)
            velocity.y = 0;

        if (Control2D.collisionInfo.above)
            velocity.y = 0;

        growthFsm.UpdateCurrentState();

        baseMovementFSM.UpdateCurrentState();

        Control2D.Move(velocity * Time.deltaTime);

        SpriteRenderer.flipX = velocity.x > 0 ? (true & !Control2D.YAxisIsInverted) : (Control2D.YAxisIsInverted);

    }

    private void LateUpdate()
    {
        SetAnimatorParameters();
    }

    private void BounceOnPlayer(Collider2D otherCol)
    {
        //if (otherCol.GetComponent<PlayerController>() != null)
        //{
        //    PlayerController otherPlayer = otherCol.GetComponent<PlayerController>();

        //    float yPosDif = entityCollider.bounds.min.y - otherPlayer.entityCollider.bounds.max.y;

        //    if (Mathf.Abs(yPosDif) < .5f)
        //    {
        //        velocity.y = 20;
        //        otherPlayer.velocity.y = 0;
        //    }
        //}
    }

    private void SetAnimatorParameters()
    {
        animator.SetBool("isGrounded", Control2D.collisionInfo.isGrounded);
        animator.SetFloat("xMove", Mathf.Abs(velocity.x));
        animator.SetFloat("yMove", velocity.y);
        animator.SetBool("invincible", invincibility.isInvincible);
    }

    public bool TouchingWall(int direction)
    {
        if (direction == 0)
            return false;

        float xPoint = direction == 1 ? this.EntityCollider.bounds.max.x : this.EntityCollider.bounds.min.x;
        Vector2 wallpoint = new Vector2(xPoint + (0.02f * direction), this.EntityCollider.bounds.center.y);

        return Physics2D.OverlapPoint(wallpoint, WhatIsWallJumpable);
    }

    public void TakeDamage(int damageToTake)
    {
        if (!invincibility.isInvincible && Health_Handler.health > 0)
        {
            invincibility.BecomeInvicible(inviciblityTime);

            Health_Handler.LoseHealth(damageToTake);
        }
        else
        { 
            // Trying to take damage while invincible.
        }
    
    }

    public void Kill()
    {
        
        growthFsm.ChangeCurrentState(DeadState);
    }

    public void Bounce(float launchHeight)
    {
        // Start a bounce couroutine to enable a period to jump higher
        velocity.y = launchHeight;
    }
}
