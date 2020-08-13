using System;
using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerBrain : Entity, ITakeDamage
{
    private bool invincible;
    private PlayerInputHandler playerInput;
    private HealthManager health;
    private PlayerData playerData;
    private PickUp collectable;

    public bool isMovingDown {get{return physicsController.Velocity.y < 0;}}


    protected override void Awake()
    {
        base.Awake();
        playerInput = this.GetComponent<PlayerInputHandler>();
        health = this.GetComponent<HealthManager>();
    }

    protected override void OnEnable()
    {
        playerData = GameManager.Instance.eachPlayersData[(playerInput.playerIndex)];

        health.OnEntityDie += Death;
    }

    protected override void OnDisable()
    {
        playerInput.playerControls.Basic.ChangeDirection.performed -= Flip;

        health.OnEntityDie -= Death;

    }

    protected override void Start()
    {
        playerInput.playerControls.Basic.ChangeDirection.performed += Flip;

    }

    public void TakeDamage(int damageToTake)
    {
        if (!invincible)
        {
            health.LoseHealth(1);
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
        // Must figure this out!
        if (collider.GetComponent<Enemy>() != null)
        {
            Enemy enemyTouched = collider.GetComponent<Enemy>();

            if(enemyTouched.transform.position.y >= transform.position.y)
            {
                TakeDamage(1);
            }
            else if(isMovingDown && enemyTouched.transform.position.y < transform.position.y)
            {
                physicsController.SetVerticalForce(50);
            }


        }
        else if (collider.GetComponent<PickUp>() != null)
        {

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

    private void Death()
    { 

    }
}
