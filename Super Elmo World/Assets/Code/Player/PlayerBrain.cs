using System;
using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerBrain : Entity, ITakeDamage, IComparable<PlayerBrain>
{
    public AnimatorOverrideController smallAnimations;
    public AnimatorOverrideController bigAnimations;
    public AnimatorOverrideController hammerAnimations;
    public AudioClip damagedSfx;

    private bool invincible;

    private HealthManager health;
    private PlayerData playerData;
    private FSM growthFsm;

    public PlayerInputHandler playerInput { get; private set; }
    public Animator playerAnimator { get; private set; }
    public bool isAlive { get; private set; }

    public bool IsMovingDown()
    {
        return (physicsController.Velocity.y < -0.1f);
    }

    protected override void Awake()
    {
        base.Awake();
        playerInput = this.GetComponent<PlayerInputHandler>();
        health = this.GetComponent<HealthManager>();
        playerAnimator = this.GetComponentInChildren<Animator>();

        
    }

    protected override void OnEnable()
    {
        health.OnHealthChange += ChangeState;

    }


    protected override void OnDisable()
    {
        playerInput.playerControls.Basic.ChangeDirection.performed -= Flip;

        health.OnHealthChange -= ChangeState;

    }
    private void IsAlive() => isAlive = true;
    private void IsDead() => isAlive = false;

    protected override void Start()
    {
        playerInput.playerControls.Basic.ChangeDirection.performed += Flip;

        if(growthFsm == null)
            growthFsm = new FSM();

        growthFsm.AddToStateList("Small", new Small(this, playerAnimator));
        growthFsm.AddToStateList("Big", new Big(this, playerAnimator));
        growthFsm.AddToStateList("Glide", new Gliding(this, playerAnimator));

        InitializePlayer();
    }

    private void Update()
    {
        playerAnimator.SetBool("isGrounded", physicsController.ControlState.isGrounded);
        playerAnimator.SetFloat("xMove", Mathf.Abs(physicsController.Velocity.x));
        playerAnimator.SetFloat("yMove", physicsController.Velocity.y);
        playerAnimator.SetBool("invincible", invincible);

        growthFsm.UpdateCurrentState();
    }

    public void TakeDamage(int damageToTake)
    {
        if (!invincible)
        {
            health.LoseHealth(1);

            AudioManager.Instance.PlaySingleRandomSfx(damagedSfx);

            if(health.currentHealth > 0)
                StartCoroutine(Invincibility(2));

            Debug.Log("Player took damage.");
        }
    }

    public void ModifySize(Vector3 newLocalScale)
    {
        // Possibly play transition animation.
        physicsController.ModifySize(newLocalScale);

    }

    protected override void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.GetComponent<Enemy>() != null)
        {
            Enemy enemyTouched = collider.GetComponent<Enemy>();

            if (enemyTouched.transform.position.y >= transform.position.y)
            {
                TakeDamage(1);
            }



        }
        else if (collider.GetComponent<PickUp>() != null)
        {

        }
        else if (collider.GetComponent<PlayerBrain>() != null)
        {
            PlayerBrain playerTouched = collider.GetComponent<PlayerBrain>();

            // Bump player if another player collides.
            if (playerTouched.transform.position.x < transform.position.x)
            {
                playerTouched.physicsController.SetHorizontalForce(-3);
            }
            else
                playerTouched.physicsController.SetHorizontalForce(3);

        }

    }

    public void Bounce()
    {
        physicsController.SetVerticalForce(15);
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

    public void ChangeState(System.Object o, GrowthEventArgs e)
    {
        switch (e.currentHealth)
        {
            case 1:
                growthFsm.ChangeCurrentState("Small");
                break;
            case 2:
                growthFsm.ChangeCurrentState("Big");
                break;
            case 3:
                SubStates(e.growthID);
                break;
            default:
                Debug.Log("CurrentHealth is out of bounds.");
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
                growthFsm.ChangeCurrentState("Glide");
                Debug.Log("Change to Cape State");
                break;
            case 2:
                // growthFsm.ChangeCurrentState("Some state");
                break;
        }

    }

    private void InitializePlayer()
    {
        growthFsm.InitializeFSM(growthFsm.GetState("Small"));

        isAlive = true;

        if (playerData == null)
            playerData = new PlayerData(0, 0, 3);

        // When intializing player for the scene, could access the Player data at this index and fill in the values. So load them at the start, and save them at the end.
        // Then UI reads from the currentPlayer.

    }

    public int CompareTo(PlayerBrain other)
    {
        if (this.playerInput.playerIndex < other.playerInput.playerIndex)
            return -1;
        else if (this.playerInput.playerIndex > other.playerInput.playerIndex)
            return 1;
        else
            return 0;
    }
}
