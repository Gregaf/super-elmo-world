using System;
using UnityEngine;
using UnityEngine.InputSystem;


[RequireComponent(typeof(Controller2D), typeof(PlayerInputHandler))]
public class PlayerController : Entity, IDamageable
{
    public AudioClip jumpSfx;

    public PlayerMoveProperties playerMoveProperties;
    public LayerMask WhatIsWallJumpable;
    public LayerMask WhatIsBackWall;
    public float inviciblityTime;

    private SpriteRenderer spriteRenderer;
    private Invincibility invincibility;

    [Header("Animator Overrides")]
    [SerializeField] private AnimatorOverrideController smallAnimations;
    [SerializeField] private AnimatorOverrideController bigAnimations;

    public PlayerInputHandler PlayerInputHandler { get; private set; }
    public Animator animator { get; private set; }
    //public FSM movementModeFsm { get; private set; }
    public FSM baseMovementFSM { get; private set; }
    public FSM growthFsm { get; private set; }

    #region PlayerBaseStates
    public Idle IdleState { get; private set; }
    public Move MoveState { get; private set; }
    public Jump JumpState { get; private set; }
    public Fall FallState { get; private set; }
    public WallSliding WallSlideState { get; private set; }
    public Crouch CrouchState { get; private set; }
    public GroundPound GroundPoundState { get; private set; }
    #endregion

    public Dead DeadState { get; private set; }


    public event Action OnShrink;


    protected override void Awake()
    {
        base.Awake();
        
        PlayerInputHandler = this.GetComponent<PlayerInputHandler>();
        invincibility = this.GetComponent<Invincibility>();

        spriteRenderer = this.GetComponentInChildren<SpriteRenderer>();
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

        growthFsm.AddToStateList((int)PlayerGrowthStates.SMALL, new Small(this, smallAnimations));
        growthFsm.AddToStateList((int)PlayerGrowthStates.BIG, new Big(this, bigAnimations));

        growthFsm.InitializeFSM((int)PlayerGrowthStates.SMALL);
        baseMovementFSM.InitializeFSM(IdleState);
        

        playerMoveProperties.CalculatePhysicsValues();
    }

    private void OnEnable()
    {
        controller2D.OnTriggerEnter += BounceOnPlayer;
    }

    private void OnDisable()
    {
        controller2D.OnTriggerEnter -= BounceOnPlayer;
    }

    private void Update()
    {
        // Stop y movement when hitting head.
        if ((controller2D.collisionInfo.below && velocity.y < 0 && !controller2D.collisionInfo.isOnSlope) || controller2D.collisionInfo.above)
            velocity.y = 0;

        if (controller2D.collisionInfo.above)
            velocity.y = 0;

        if (Input.GetButtonDown("Jump"))
            AudioManager.Instance.PlaySingleSfx(jumpSfx);

        baseMovementFSM.UpdateCurrentState();   

        controller2D.Move(velocity * Time.deltaTime);

        spriteRenderer.flipX = velocity.x > 0 ? (true & !controller2D.YAxisIsInverted) : (controller2D.YAxisIsInverted);

    }

    private void LateUpdate()
    {
        SetAnimatorParameters();
    }

    private void BounceOnPlayer(Collider2D otherCol)
    {
        if (otherCol.GetComponent<PlayerController>() != null)
        {
            PlayerController otherPlayer = otherCol.GetComponent<PlayerController>();

            float yPosDif = entityCollider.bounds.min.y - otherPlayer.entityCollider.bounds.max.y;

            if (Mathf.Abs(yPosDif) < .5f)
            {
                velocity.y = 20;
                otherPlayer.velocity.y = 0;
            }
        }
    }

    private void SetAnimatorParameters()
    {
        animator.SetBool("isGrounded", controller2D.collisionInfo.isGrounded);
        animator.SetFloat("xMove", Mathf.Abs(velocity.x));
        animator.SetFloat("yMove", velocity.y);
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

    public void TakeDamage(int damageToTake)
    {
        if (!invincibility.isInvincible)
        {
            invincibility.BecomeInvicible(inviciblityTime);

            OnShrink?.Invoke();
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

}
