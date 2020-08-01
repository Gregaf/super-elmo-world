using System.Collections;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Threading;
using UnityEditor;
using UnityEditor.Tilemaps;
using UnityEngine;
using UnityEngine.InputSystem;

public enum MovementMode
{
    Ground,
    Flying,
    Swimming
}
public class PlayerMovement : MonoBehaviour
{

    private PlayerInput playerInput;
    private CharacterController2D controller2D;

    private void Awake()
    {
        controller2D = this.GetComponent<CharacterController2D>();
    }

    private void OnEnable()
    {
    }

    private void OnDisable()
    {
    }

    public void Jump()
    {
    }

}
