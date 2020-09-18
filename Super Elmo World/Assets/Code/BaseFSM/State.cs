using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public abstract class State
{
    public abstract void Enter();

    public abstract void Exit();

    public abstract void Tick();

    public abstract void OnTriggerEnter2D(Collider2D collider2D);

}

