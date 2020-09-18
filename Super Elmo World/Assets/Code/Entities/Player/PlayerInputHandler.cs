using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Users;

public class PlayerInputHandler : InputHandler
{
    public int playerIndex;
    public InputUser inputUser;
    public InputDevice deviceBeingUsed = null;
    public bool IsMovingHorizontally { get { return (Mathf.Abs(MovementInput.x) > 0); } }
    public bool JumpButtonActive { get; private set; }
    public bool RunButtonActive { get; private set; }
    public PlayerControls playerControls { get; private set; }

    private void Awake()
    {
        playerControls = new PlayerControls();

        playerControls.Enable();
    }

    private void OnEnable()
    {
        playerControls.Enable();

        playerControls.Basic.Jump.started += Jumping;
        playerControls.Basic.Jump.canceled += Jumping;

        playerControls.Basic.Run.started += Running;
        playerControls.Basic.Run.canceled += Running;

    }
    private void OnDisable()
    {
        playerControls.Disable();

        playerControls.Basic.Jump.started -= Jumping;
        playerControls.Basic.Jump.canceled -= Jumping;

        playerControls.Basic.Run.started -= Running;
        playerControls.Basic.Run.canceled -= Running;

    }

    private void Start()
    {

    }

    private void Running(InputAction.CallbackContext context)
    {
        if (context.started)
            RunButtonActive = true;
        else if (context.canceled)
            RunButtonActive = false;
    }

    private void Jumping(InputAction.CallbackContext context)
    {
        if (context.started)
            JumpButtonActive = true;
        else if (context.canceled)
            JumpButtonActive = false;
    }

    private void Update()
    {
        MovementInput = playerControls.Basic.Move.ReadValue<Vector2>();
        

    }

    public void DisableControls()
    {
        playerControls.Disable();
    }

    public void EnableControls()
    {
        playerControls.Enable();
    }

}
