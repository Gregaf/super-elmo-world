using System;
using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerBrain : Entity, ITakeDamage
{
    private bool invincible;
    private PlayerInputHandler playerInput;
    private PlayerData playerData;
    private PickUp collectable;

    protected override void Awake()
    {
        base.Awake();
        playerInput = this.GetComponent<PlayerInputHandler>();
    }

    protected override void OnEnable()
    {
        playerData = GameManager.Instance.eachPlayersData[(playerInput.playerIndex)];
        Debug.Log(playerData);

    }

    protected override void OnDisable()
    {
        playerInput.playerControls.Basic.ChangeDirection.performed -= Flip;
    }

    protected override void Start()
    {
        playerInput.playerControls.Basic.ChangeDirection.performed += Flip;

    }

    public void TakeDamage(int damageToTake)
    {
        if (!invincible)
        {
            healthManager.LoseHealth(damageToTake);
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
            Enemy interactedWith = collider.GetComponent<Enemy>();

            if (physicsController.Velocity.y < 0 && transform.position.y > interactedWith.transform.position.y)
            {
                interactedWith.TakeDamage(1);
                physicsController.SetVerticalForce(15);
            }

        }
        else if (collider.GetComponent<PickUp>() != null)
        {
            collectable = collider.GetComponent<PickUp>();

            collectable.pickUpEventHandler += UpdatePlayerData;
            collectable.pickUpEventHandler += GameManager.Instance.PlayerDataUpdated;
        }

    }

    private void UpdatePlayerData(System.Object o, PickUpEventArgs pickUp)
    {
        playerData.AddCoins(pickUp.coinValue);
        playerData.AddScore(pickUp.scoreValue);
        playerData.AddLives(pickUp.livesValue);

        // Maybe fire an event OnUpdatePlayerData so UI can update player onscreen data.
        
        collectable.pickUpEventHandler -= UpdatePlayerData;
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

    private void Death(System.Object sender, EventArgs e)
    { 

    }
}
