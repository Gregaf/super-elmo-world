using System.Dynamic;
using UnityEngine;
using UnityEngine.XR;

public class PlayerInput : MonoBehaviour
{
    public bool HandleInput { get; set; }
    public Vector2 MovementInput { get; private set; }
    public bool JumpButtonPressed { get; private set; }
    public bool JumpButtonHeld { get; private set; }
    public bool RunButtonHeld { get; private set; }

    public bool PauseButtonPressed { get; private set; }

    public delegate void OnPause();
    public static event OnPause onPause;

    public void Start()
    {
        HandleInput = true;
    }

    private void Update()
    {
        PauseButtonPressed = Input.GetKeyDown(KeyCode.Escape);

        if (PauseButtonPressed)
        {
            onPause.Invoke();
        }

        MovementInput = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));

        JumpButtonPressed = Input.GetButtonDown("Jump");
        JumpButtonHeld = Input.GetButton("Jump");

        // Must change to a mapped button.
        RunButtonHeld = Input.GetKey(KeyCode.X);

    }

}
