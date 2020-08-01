using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class CubeController : MonoBehaviour
{

    Vector2 movement = Vector2.zero;
    float moveSpeed = 10f;

    private void Update()
    {
        Move();
    }

    private void Move()
    {
        Vector3 movement = new Vector3(this.movement.x, 0, this.movement.y) * moveSpeed * Time.deltaTime;
        transform.Translate(movement);
    }


    private void OnMove(InputValue value)
    {
        movement = value.Get<Vector2>();
    }

    private void OnMoveUp()
    {
        transform.Translate(transform.up);
    }

    private void OnMoveDown()
    {
        transform.Translate(-transform.up);
    }
}
