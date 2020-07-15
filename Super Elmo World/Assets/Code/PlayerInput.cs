using System.Dynamic;
using UnityEngine;
using UnityEngine.XR;

public class PlayerInput : MonoBehaviour
{
    public bool HandleInput { get; set; }
    public Vector2 MovementInput { get; private set; }
    public bool JumpInput { get; private set; }



    public void Start()
    {
        HandleInput = true;
    }

    private void Update()
    {
        MovementInput = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        JumpInput = Input.GetButtonDown("Jump");

    }

}
