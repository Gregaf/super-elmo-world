using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Door : MonoBehaviour
{
    protected enum DoorStates
    { 
        Closed,
        Open
    }

    [SerializeField] protected DoorStates currentDoorState;
    protected BoxCollider2D objectCollider;
    protected Animator doorAnimator;

    public event Action OnDoorOpen;

    protected virtual void Awake()
    {
        objectCollider = this.GetComponent<BoxCollider2D>();
    
    }

    protected virtual void Start()
    {
        // Default to a closed door.
        currentDoorState = DoorStates.Closed;
    }

    protected void Open()
    {
        // Play Opening animation

        OnDoorOpen?.Invoke();
        objectCollider.enabled = false;
        currentDoorState = DoorStates.Open;
    }

    protected void Close()
    {
        // Play closing animation
        objectCollider.enabled = true;
        currentDoorState = DoorStates.Closed;
    }

}
