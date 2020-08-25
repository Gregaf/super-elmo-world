using System;
using UnityEngine;
using UnityEngine.InputSystem;
using BaseGame;


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
    private PlayerController playerBrain;
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
        playerBrain = this.GetComponent<PlayerController>();
        playerInput = this.GetComponent<PlayerInputHandler>();
        playerAnimator = this.GetComponentInChildren<Animator>();

        movementState = new FSM();
        movementState.AddToStateList("Ground", new GroundMovement(playerInput, controller2D, groundMoveProperties, playerAnimator));
        movementState.AddToStateList("Flying", new FlyingMovement());


        
    }

    private void OnEnable()
    {
        playerBrain.OnPlayerDeath += DisableMovementFsm;
    }

    private void OnDisable()
    {
        playerBrain.OnPlayerDeath -= DisableMovementFsm;
    }

    private void Start()
    {
        movementState.InitializeFSM(movementState.GetState("Ground"));

    }

    private void Update()
    {
        movementState.UpdateCurrentState();

    }

    private void DisableMovementFsm()
    {
        movementState.isActive = false;
    }
    private void EnableMovementFsm()
    {
        movementState.isActive = true;
    }
}
