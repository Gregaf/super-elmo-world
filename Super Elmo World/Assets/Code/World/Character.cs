using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    public float moveSpeed;
    public LayerMask whatIsNode;

    private Node currentNode;

    private PlayerInputHandler playerInput;
    private FSM characterFsm;

    private MovingState movingState;
    private WaitingState waitingState;

    private void Awake()
    {
        characterFsm = new FSM();

        movingState = new MovingState();
        waitingState = new WaitingState();

    }


    private void Update()
    {
        characterFsm.UpdateCurrentState();

    }

    public Node CheckNode()
    {
        Node newNode = null;

        newNode = Physics2D.OverlapBox(transform.position, new Vector2(1, 1), 0, whatIsNode).GetComponent<Node>();

        return newNode;
    }

}
