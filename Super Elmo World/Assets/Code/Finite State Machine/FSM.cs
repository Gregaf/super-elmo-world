using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using UnityEngine;

public class FSM
{
    public bool isActive = true;
    public State CurrentState { get; private set; }
    public State PreviousState { get; private set; }

    private Dictionary<string, State> listedStates = new Dictionary<string, State>();
    

    public void InitializeFSM(State beginnngState)
    {

        CurrentState = beginnngState;
    }

    public void ChangeCurrentState(State newState)
    {
        CurrentState.Exit();
        PreviousState = CurrentState;

        CurrentState = newState;
        CurrentState.Enter();
    }

    public void ReturnToPreviousState()
    {
        CurrentState.Exit();
        State temp = CurrentState;

        CurrentState = PreviousState;
        CurrentState.Enter();

        PreviousState = temp;
    }

    // Takes a collection of states and will add each state from the collection to the listedStates dictionary.
    public void PopulateStatesList(Collection<State> newStates)
    {
        foreach (State newstate in newStates)
        {
            listedStates.Add(newstate.ToString(), newstate);
        }
    }

    
    public void AddToStateList(string key, State newState)
    {
        listedStates.Add(key, newState);
    }

    public State GetState(string key)
    {
        return listedStates[key];
    }

    public void UpdateCurrentState()
    {
        if (isActive)
            CurrentState.StateUpdate();
        else
            Debug.Log("FSM: " + this + " is not active.");

    }


}
