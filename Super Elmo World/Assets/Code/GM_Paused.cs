using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GM_Paused : State
{
    
    // Unless I can figure out how to raise an event from my states, must pass in the UIController and call Pause/Unpause
    public GM_Paused(FSM ownerFsm)
    {
        this.ownerFsm = ownerFsm;
    }

    public override void Enter()
    {
        // Send event to enable pause screen and set timescale to 0
    }

    public override void Exit()
    {
        // Send event to disable pause screen and set timescale back to 1
    }

    public override void StateUpdate()
    {

    }
}
