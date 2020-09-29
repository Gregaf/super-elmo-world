using UnityEngine;
using UnityEngine.InputSystem;


public class WallSliding : PlayerState
{

    private int wallDirection = 0;
    private float timeTillUnstick = 0.25f;
    private float wallStickTimer;
    private bool unstickTimerStart;

    private float fStore;

    public WallSliding(PlayerController playerEntity) : base(playerEntity)
    {

    }

    public override void Enter()
    {
        wallDirection = controller2D.collisionInfo.left ? -1 : 1;

        wallStickTimer = timeTillUnstick;

        Debug.Log(wallStickTimer);  

        unstickTimerStart = false;

        playerEntity.velocity.y = 1;

        playerEntity.animator.SetBool("WallSliding", true);

        playerInput.playerControls.Basic.Jump.performed += LaunchFromWall;
    }

    public override void Exit()
    {
        playerInput.playerControls.Basic.Jump.performed -= LaunchFromWall;

        playerEntity.animator.SetBool("WallSliding", false);
    }

    // TODO: Set up a wall dismount force, and clean up the code.
    public override void Tick()
    {

        if (playerInput.MovementInput.x != wallDirection)
        {
            Debug.Log("Start unstick");
            unstickTimerStart = true;
        }

        if (unstickTimerStart)
            wallStickTimer -= Time.deltaTime;


        if (wallStickTimer <= 0 || !(controller2D.collisionInfo.left || controller2D.collisionInfo.right))
        {
            playerEntity.velocity = new Vector2((-wallDirection * playerMove.wallLaunchVelocity.x) / 3, playerMove.wallLaunchVelocity.y / 4);
            playerEntity.baseMovementFSM.ChangeCurrentState(playerEntity.FallState);
        }

        if (controller2D.collisionInfo.isGrounded)
        {
            playerEntity.baseMovementFSM.ChangeCurrentState(playerEntity.IdleState);
        }

        playerEntity.velocity.y = Mathf.SmoothDamp(playerEntity.velocity.y, -playerMove.walkSpeed, ref fStore, .25f);
    }

    private void LaunchFromWall(InputAction.CallbackContext context)
    {
        playerEntity.velocity = new Vector2(-wallDirection * playerMove.wallLaunchVelocity.x, playerMove.wallLaunchVelocity.y);
        playerEntity.baseMovementFSM.ChangeCurrentState(playerEntity.FallState);
        wallStickTimer = 0;
    }
}
