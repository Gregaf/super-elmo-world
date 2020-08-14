using System;
using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerBrain : Entity, ITakeDamage
{
    public AnimatorOverrideController smallAnimations;
    public AnimatorOverrideController bigAnimations;
    public AnimatorOverrideController hammerAnimations;

    private bool invincible;
    private PlayerInputHandler playerInput;
    private PlayerMovement playerMovement;
    private HealthManager health;
    private PlayerData playerData;
    private PickUp collectable;
    private FSM growthFsm;
    private Animator animator;

    public bool IsMovingDown()
    {
        return (physicsController.Velocity.y < -0.1f);
    }

    protected override void Awake()
    {
        base.Awake();
        playerInput = this.GetComponent<PlayerInputHandler>();
        playerMovement = this.GetComponent<PlayerMovement>();
        health = this.GetComponent<HealthManager>();
        animator = this.GetComponentInChildren<Animator>();

        growthFsm = new FSM();

        growthFsm.AddToStateList("Small", new Small(this, animator));
        growthFsm.AddToStateList("Big", new Big(this, animator));
        growthFsm.AddToStateList("Glide", new Gliding(this, animator));

        
    }

    protected override void OnEnable()
    {
        playerData = GameManager.Instance.eachPlayersData[(playerInput.playerIndex)];

        health.OnHealthChange += ChangeState;
    }

    protected override void OnDisable()
    {
        playerInput.playerControls.Basic.ChangeDirection.performed -= Flip;

        health.OnHealthChange -= ChangeState;
    }

    protected override void Start()
    {
        playerInput.playerControls.Basic.ChangeDirection.performed += Flip;
        
        growthFsm.InitializeFSM(growthFsm.GetState("Small"));   
    }

    private void Update()
    {
        animator.SetBool("isGrounded", physicsController.ControlState.isGrounded);
        animator.SetFloat("xMove", Mathf.Abs(physicsController.Velocity.x));
        animator.SetFloat("yMove", physicsController.Velocity.y);
        animator.SetBool("invincible", invincible);

        growthFsm.UpdateCurrentState();
    }

    public void TakeDamage(int damageToTake)
    {
        if (!invincible)
        {
            health.LoseHealth(1);

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
}
