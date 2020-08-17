using System;
using UnityEngine;
using UnityEngine.InputSystem;


[Serializable]
public class HelplessMoveProperties
{
    public Transform spawnPoint;

}

[Serializable]
public class DeathMoveProperties
{ 


}

[Serializable]
public class GroundMoveProperties
{
    public float baseSpeed;
    public float runSpeed;
    public float runAcceleration;
    public float airAcceleration;
    public float groundAcceleration;
    [Space(10)]
    public AudioClip jumpSfx;
    public float jumpTime;
    public float jumpHeight;
    public float gravity;
    public float fallMultiplier;
}

[Serializable]
public class FlyingMoveProperties
{
    public float moveSpeed;
    public float levelSpeed;
}

public class PlayerMovement : MonoBehaviour
{
    private PlayerInputHandler playerInput;
    private HealthManager health;
    private CharacterController2D controller2D;
    public FSM movementState { get; private set; }
    private Animator playerAnimator;
    [SerializeField] private GroundMoveProperties groundMoveProperties = null;
    [SerializeField] private FlyingMoveProperties flyingMoveProperties = null;
    [SerializeField] private DeathMoveProperties deathMoveProperties = null;
    [SerializeField] private HelplessMoveProperties helplessMoveProperties = null;

    private void Awake()
    {
        controller2D = this.GetComponent<CharacterController2D>();
        playerInput = this.GetComponent<PlayerInputHandler>();
        health = this.GetComponent<HealthManager>();
        playerAnimator = this.GetComponentInChildren<Animator>();

        movementState = new FSM();
        movementState.AddToStateList("Ground", new GroundMovement(movementState, playerInput, controller2D, groundMoveProperties, playerAnimator));
        movementState.AddToStateList("Flying", new FlyingMovement());
        movementState.AddToStateList("Death", new DeathMovement(movementState, playerInput, controller2D, gameObject, deathMoveProperties));
        movementState.AddToStateList("Helpless", new HelplessMovement(helplessMoveProperties, controller2D, playerInput, playerAnimator));

    }

    private void OnEnable()
    {
        health.OnEntityDie += Die;
    }

    private void OnDisable()
    {
        health.OnEntityDie -= Die;
    }

    private void Start()
    {
        movementState.InitializeFSM(movementState.GetState("Ground"));

    }

    private void Update()
    {
        movementState.UpdateCurrentState();

    }

    private void Die()
    {
        movementState.ChangeCurrentState("Death");
    }
}
