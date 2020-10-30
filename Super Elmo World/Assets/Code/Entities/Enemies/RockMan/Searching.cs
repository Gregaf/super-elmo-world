using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Searching : State
{
    private float searchRadius = 4f;
    private Collider2D[] targets;
    private LayerMask whoToSearchFor;
    private Transform target;
    Rocker owner;

    public Searching(Rocker owner, Transform target, LayerMask whoToSearchFor)
    {
        this.owner = owner;
        this.target = target;
        this.whoToSearchFor = whoToSearchFor;

        targets = new Collider2D[4];
    }

    public override void Enter()
    {
        owner.target = null;
        // Play animation to look around
    }

    public override void Exit()
    {

    }

    public override void OnTriggerEnter2D(Collider2D collider2D)
    {
        throw new System.NotImplementedException();
    }

    public override void Tick()
    {
        int n = Physics2D.OverlapCircleNonAlloc(owner.transform.position, searchRadius, targets, whoToSearchFor);

        Debug.Log(n);

        GetClosestTarget(n);
    }

    private void GetClosestTarget(int n)
    {
        if (n == 0)
            return;
        else if (n == 1)
        {
            owner.target = targets[0].transform;
            owner.EnemyFsm.ChangeCurrentState(owner.AttackingState);
            return;
        }

        float lowestDistance = 100f;
        for (int i = 0; i < n; i++)
        {
            float distance = Vector2.Distance(owner.transform.position, targets[i].transform.position);

            if (distance < lowestDistance)
            {
                lowestDistance = distance;
                owner.target = targets[i].transform;
            }
        }

        // Create a delay before switching since itd be immediate cylce if goes from attacking back to searching.
        owner.EnemyFsm.ChangeCurrentState(owner.AttackingState);
    }
}
