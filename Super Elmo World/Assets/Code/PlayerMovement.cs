using System.Collections;
using System.Collections.Generic;
using UnityEditor.Tilemaps;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public enum MovementMode
    { 
        Ground,
        Flying
    }

    MovementMode mode;

    private CharacterController2D physicsController;
    private PlayerInput playerInput;

    private void Awake()
    {
        physicsController = this.GetComponent<CharacterController2D>();
        playerInput = this.GetComponent<PlayerInput>();
    }

    private void Start()
    {
        

    }

    private void Update()
    {


    }


    private void Move()
    {
        
    }

    private void Jump()
    { 
    
    }

}
