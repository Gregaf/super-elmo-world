using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EDead : IState
{
    private GameObject ownerToDie;

    public EDead(GameObject ownerToDie)
    {
        this.ownerToDie = ownerToDie;
    }

    public void Enter()
    {
        GameObject.Destroy(ownerToDie);
    }

    public void Exit()
    {

    }

    public void Tick()
    {

    }

    public void OnTriggerEnter(Collider2D collider2D)
    {

    }
}
