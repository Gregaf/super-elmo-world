using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dead : IState
{
    private GameObject owner;
    private PlayerInputHandler playerInput;
    private PlayerData playerData;
    private CharacterController2D controller2D;

    private Camera gameCamera;

    public Dead(GameObject owner, PlayerInputHandler playerInput, PlayerData playerData, CharacterController2D controller2D)
    {
        this.owner = owner;
        this.playerInput = playerInput;
        this.playerData = playerData;
        this.controller2D = controller2D;

        gameCamera = GameObject.FindObjectOfType<Camera>();
    }

    public void Enter()
    {
        playerInput.DisableControls();
        controller2D.HandleCollision = false;

        controller2D.SetForce(new Vector2(0f, 15f));
    }

    public void Exit()
    {
        playerInput.EnableControls();

        controller2D.SetVerticalForce(0f);

    }

    public void OnTriggerEnter(Collider2D collider2D)
    {
    }

    public void Tick()
    {
        controller2D.SetVerticalForce(Mathf.Lerp(controller2D.Velocity.y, -25, Time.deltaTime));


    }

    private Vector2 GetDeathBoundary()
    {
        Vector2 bounds = Vector2.zero;
        bounds.y = gameCamera.orthographicSize + 2;
        bounds.x = (bounds.y) * gameCamera.aspect + 2;

        return bounds;
    }

    private Vector3 Rotate(Vector3 currentRotation, float spinSpeed)
    {
        Vector3 newEulerAngles = currentRotation;
        newEulerAngles.z += spinSpeed * Time.deltaTime;

        return newEulerAngles;
    }
}
