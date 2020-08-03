using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using UnityEngine;

public class FSM
{
    public bool isActive = true;
    public IState CurrentState { get; private set; }
    public IState PreviousState { get; private set; }

    private Dictionary<string, IState> listedStates = new Dictionary<string, IState>();
    

    public void InitializeFSM(IState beginnngState)
    {

        ChangeCurrentState(beginnngState);
    }

    public void ChangeCurrentState(IState newState)
    {
        CurrentState?.Exit();
        PreviousState = CurrentState;

        CurrentState = newState;
        CurrentState.Enter();
    }
    public void ChangeCurrentState(string stateName)
    {
        IState newState = this.listedStates[stateName];

        CurrentState?.Exit();
        PreviousState = CurrentState;

        CurrentState = newState;
        CurrentState.Enter();
    }

    public void ReturnToPreviousState()
    {
        CurrentState.Exit();
        IState temp = CurrentState;

        CurrentState = PreviousState;
        CurrentState.Enter();

        PreviousState = temp;
    }

    // Takes a collection of states and will add each state from the collection to the listedStates dictionary.
    public void PopulateStatesList(Collection<IState> newStates)
    {
        foreach (IState newstate in newStates)
        {
            listedStates.Add(newstate.ToString(), newstate);
        }
    }

    
    public void AddToStateList(string key, IState newState)
    {
        listedStates.Add(key, newState);
    }

    public IState GetState(string key)
    {
        return listedStates[key];
    }

    public void UpdateCurrentState()
    {
        if (isActive)
            CurrentState?.StateUpdate();
        else
            Debug.Log("FSM: " + this + " is not active.");

    }


}
