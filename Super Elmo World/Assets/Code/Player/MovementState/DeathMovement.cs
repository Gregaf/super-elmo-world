using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathMovement : IState
{
    private FSM ownerFsm;
    private PlayerInputHandler playerInput;
    private CharacterController2D controller2D;
    private GameObject ownerGO;
    private DeathMoveProperties deathMoveProperties;

    private Camera gameCamera;
    private float movementRate = 25;
    private Transform aesthetic;
    private PlayerData playerData;

    public DeathMovement(FSM ownerFsm, PlayerInputHandler playerInput, CharacterController2D controller2D, GameObject ownerGO, DeathMoveProperties deathMoveProperties)
    {
        this.ownerFsm = ownerFsm;
        this.playerInput = playerInput;
        this.controller2D = controller2D;
        this.ownerGO = ownerGO;
        this.deathMoveProperties = deathMoveProperties;

        gameCamera = GameObject.FindObjectOfType<Camera>();
        aesthetic = playerInput.gameObject.transform.GetChild(0).transform;
        playerData = GameManager.Instance.eachPlayersData[playerInput.playerIndex];
    }

    public void Enter()
    {
        playerInput.playerControls.Disable();
        ownerGO.GetComponent<Rigidbody2D>().simulated = false;
        controller2D.HandleCollision = false;
        controller2D.SetHorizontalForce(0f);
        controller2D.SetVerticalForce(10f);
    }

    public void Exit()
    {
        playerInput.playerControls.Enable();
        ownerGO.GetComponent<Rigidbody2D>().simulated = true;
        // Review maybe disable and leave it up to other movestates to enable collision.
        controller2D.HandleCollision = true;
    }

    public void StateUpdate()
    {
        controller2D.SetVerticalForce(Mathf.Lerp(controller2D.Velocity.y, movementRate * Vector2.down.y, Time.deltaTime));

        aesthetic.localEulerAngles = Rotate(aesthetic.localEulerAngles, 450f);

        if (playerInput.gameObject.transform.position.y < (gameCamera.transform.position.y - GetDeathBoundary().y))
        {
            Debug.Log($"{playerInput.gameObject} has fallen outside of the death boundaries.");

            if (playerData.GetLives() > 0)
            {
                playerData.LoseLives(1);
                ownerFsm.ChangeCurrentState("Helpless");

            }
            else
            {
                ownerGO.SetActive(false);
            }
        }

    }



    public Vector2 GetDeathBoundary()
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
