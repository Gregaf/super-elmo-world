using System;
using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : Entity, IDamageable, IComparable<PlayerController>, IRanked, IBounceable
{
    public AnimatorOverrideController smallAnimations;
    public AnimatorOverrideController bigAnimations;
    public AnimatorOverrideController hammerAnimations;
    public AudioClip damagedSfx;

    private bool invincible;

    private FSM rankedFsm;
    private Carry carryAction;

    [Header("Bounce Properties")]
    [SerializeField] private float bounceTimeFrame = 0.25f;

    public PlayerInputHandler playerInput { get; private set; }
    public Animator playerAnimator { get; private set; }
    public PlayerData playerData { get; private set; }
    public bool isAlive { get; private set; }

    public event Action OnPlayerDeath;

    public bool IsMovingDown()
    {
        return (controller2D.Velocity.y < -0.1f);
    }

    protected override void Awake()
    {
        base.Awake();
        playerInput = this.GetComponent<PlayerInputHandler>();
        playerData = this.GetComponent<PlayerData>();
        playerAnimator = this.GetComponentInChildren<Animator>();
        carryAction = this.GetComponent<Carry>();
        
    }

    protected override void OnEnable()
    {

    }


    protected override void OnDisable()
    {
        playerInput.playerControls.Basic.ChangeDirection.performed -= Flip;

    }

    private void PlayerDied() => isAlive = false;

    private void PlayerRespawned() => isAlive = true;

    protected override void Start()
    {
        playerInput.playerControls.Basic.ChangeDirection.performed += Flip;


        if (rankedFsm == null)
            rankedFsm = new FSM();

        rankedFsm.AddToStateList("Dead", new Dead(this.gameObject, playerInput, null, controller2D));
        rankedFsm.AddToStateList("Small", new Small(this, playerAnimator));
        rankedFsm.AddToStateList("Big", new Big(this, playerAnimator));
        rankedFsm.AddToStateList("Glide", new Gliding(this, playerAnimator));

        rankedFsm.InitializeFSM(rankedFsm.GetState("Small"));

        InitializePlayer();
    }

    private void Update()
    {
        playerAnimator.SetBool("isGrounded", controller2D.ControlState.isGrounded);
        playerAnimator.SetFloat("xMove", Mathf.Abs(controller2D.Velocity.x));
        playerAnimator.SetFloat("yMove", controller2D.Velocity.y);
        playerAnimator.SetBool("invincible", invincible);

        rankedFsm.UpdateCurrentState();

        if (carryAction.isCarryingObject)
        {
            if (!playerInput.RunButtonActive)
                carryAction.LaunchLiftable(playerInput.MovementInput, 15f);
        }
    }

    public void TakeDamage(int damageToTake)
    {
        if (!invincible)
        {
            AudioManager.Instance.PlaySingleRandomSfx(damagedSfx);

            if (rankedFsm.CurrentState != rankedFsm.GetState("Small"))
                StartCoroutine(Invincibility(1.5f));

            Demote();

            Debug.Log("Player took damage.");
        }
    }

    public void ModifySize(Vector3 newLocalScale)
    {
        // Possibly play transition animation.
        controller2D.ModifySize(newLocalScale);

    }

    protected override void OnTriggerEnter2D(Collider2D collider2D)
    {
        rankedFsm.CurrentState.OnTriggerEnter(collider2D);

        if (collider2D.GetComponent<Enemy>() != null)
        {
            Enemy enemyTouched = collider2D.GetComponent<Enemy>();

            if (enemyTouched.transform.position.y >= transform.position.y)
            {
                TakeDamage(1);
            }
        }
        else if (collider2D.GetComponent<ILiftable>() != null)
        {
            if (playerInput.RunButtonActive && !carryAction.isCarryingObject)
            {
                carryAction.PickUpLiftable(collider2D.GetComponent<ILiftable>());
            }
        }

    }

    public void Bounce(float launchHeight)
    {
        StartCoroutine(BounceWindow(launchHeight));
    }

    IEnumerator BounceWindow(float bounceHeight)
    {
        float timer = 0;

        controller2D.SetVerticalForce(bounceHeight);

        while (timer < bounceTimeFrame)
        {
            timer += Time.deltaTime;

            if (playerInput.JumpButtonActive)
            {
                controller2D.SetVerticalForce(bounceHeight * 1.5f);
            }

            yield return null;
        }
    }

    private IEnumerator Invincibility(float invicibilityTime)
    {
        invincible = true;

        yield return new WaitForSeconds(invicibilityTime);

        invincible = false;
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


    private void InitializePlayer()
    {
        isAlive = true;

        // When intializing player for the scene, could access the Player data at this index and fill in the values. So load them at the start, and save them at the end.
        // Then UI reads from the currentPlayer.

    }

    public int CompareTo(PlayerController other)
    {
        if (this.playerInput.playerIndex < other.playerInput.playerIndex)
            return -1;
        else if (this.playerInput.playerIndex > other.playerInput.playerIndex)
            return 1;
        else
            return 0;
    }

    public void Promote(int level, int subLevel)
    {
        switch (level)
        {
            case 2:
                rankedFsm.ChangeCurrentState("Big");
                break;
            case 3:
                SubStates(subLevel);
                break;
            default:
                Debug.Log($"Undefined case: {level}");
                break;
        }

    }

    private void SubStates(int itemID)
    {
        switch (itemID)
        {
            case 0:
                // growthFsm.ChangeCurrentState("Projectile");
                Debug.Log("Change to Fire State");
                break;
            case 1:
                rankedFsm.ChangeCurrentState("Glide");
                Debug.Log("Change to Cape State");
                break;
            case 2:
                // growthFsm.ChangeCurrentState("Some state");
                break;
        }

    }

    public void Demote()
    {
        if (rankedFsm.CurrentState == rankedFsm.GetState("Small"))
        {
            OnPlayerDeath?.Invoke();
            isAlive = false;
            rankedFsm.ChangeCurrentState("Dead");
        }
        else if (rankedFsm.CurrentState == rankedFsm.GetState("Big"))
        {
            rankedFsm.ChangeCurrentState("Small");
        }
        else
        {
            rankedFsm.ChangeCurrentState("Big");
        }

    }

    public void Kill()
    {
        if (!isAlive)
            return;

        isAlive = false;
        // Switch to dead state.
        OnPlayerDeath?.Invoke();
        rankedFsm.ChangeCurrentState("Dead");
    }
}
