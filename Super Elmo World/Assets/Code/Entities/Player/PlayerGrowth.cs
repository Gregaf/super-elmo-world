using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerGrowth : MonoBehaviour, IDamageable
{
    [Header("Animator Overrides")]
    [SerializeField] private AnimatorOverrideController smallAnimations;
    [SerializeField] private AnimatorOverrideController bigAnimations;

    public LayerMask damagedByWho;

    private PlayerController playerController;
    public FSM growthFsm;
    private Invincibility invincibility;

    public event Action OnShrink;

    private void Awake()
    {
        playerController = this.GetComponent<PlayerController>();
        invincibility = this.GetComponent<Invincibility>();
        growthFsm = new FSM();

    }

    private void OnEnable()
    {
    }

    private void OnDisable()
    {
        playerController.controller2D.OnTriggerEnter -= OnTouchEnemy;
    }

    private void Start()
    {
        playerController.controller2D.OnTriggerEnter += OnTouchEnemy;

        growthFsm.AddToStateList((int)PlayerGrowthStates.SMALL, new Small(playerController, smallAnimations, this));
        growthFsm.AddToStateList((int)PlayerGrowthStates.BIG, new Big(playerController, bigAnimations, this));

        growthFsm.InitializeFSM((int)PlayerGrowthStates.SMALL);
    }

    private void Update()
    {
        growthFsm.UpdateCurrentState();
    }

    public void GrowTo(PlayerGrowthStates growthState)
    {
        if (growthFsm.IsCurrentState((int)growthState))
        {
            // ALready currently in that state...
            // Maybe add some points
            return;
        }

        growthFsm.ChangeCurrentState((int)growthState);
    }

    private void OnTouchEnemy(Collider2D collider2D)
    {
        Enemy collidingWith = collider2D.GetComponent<Enemy>();

        if (transform.position.y > collider2D.transform.position.y && collidingWith.isVunerable)
        {
            //playerController.Controller2D.SetVerticalForce(15f);
        }
        else
        {
            TakeDamage(1);
        }
    }

    public void TakeDamage(int damageToTake)
    {
        if (invincibility.isInvincible)
            return;

        invincibility.BecomeInvicible(1.25f);

        OnShrink?.Invoke();
    }

    public void Kill()
    {
        // Change to the death state.
    }
}
