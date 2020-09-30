using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class FSM
{
    public bool isActive = true;
    public State CurrentState { get; private set; }
    public State PreviousState { get; private set; }

    public Dictionary<int, State> storedStates = new Dictionary<int, State>();

    private bool canUpdate;
    public void InitializeFSM(int beginnngState)
    {
        ChangeCurrentState(beginnngState);

        isActive = true;

        canUpdate = true;
    }

    public void InitializeFSM(State beginnngState)
    {
        ChangeCurrentState(beginnngState);

        isActive = true;
    }

    public void ChangeCurrentState(State newState)
    {
        canUpdate = false;

        CurrentState?.Exit();
        PreviousState = CurrentState;

        CurrentState = newState;
        CurrentState.Enter();

        canUpdate = true;
    }
    public void ChangeCurrentState(int stateName)
    {
        State newState = this.storedStates[stateName];

        canUpdate = false;

        CurrentState?.Exit();
        PreviousState = CurrentState;

        CurrentState = newState;
        CurrentState.Enter();

        canUpdate = true;
    }

    public bool IsCurrentState(int stateName)
    {
        if (CurrentState.Equals(storedStates[stateName]))
            return true;
        else
            return false;
    }

    public void ReturnToPreviousState()
    {
        CurrentState.Exit();
        State temp = CurrentState;

        CurrentState = PreviousState;
        CurrentState.Enter();

        PreviousState = temp;
    }

    public void AddToStateList(int key, State newState)
    {
        storedStates.Add(key, newState);
    }

    public State GetState(int key)
    {
        return storedStates[key];
    }

    public void UpdateCurrentState()
    {
        if (isActive)
            CurrentState?.Tick();
        else
            Debug.Log($"{this} FSM is not active!");
    }


}

