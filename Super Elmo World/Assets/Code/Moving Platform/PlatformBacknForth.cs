using System;
using UnityEngine;

public class PlatformBacknForth : IState
{
    private FSM ownerFsm;
    private GameObject platform;
    private Transform[] path;
    private float speed;

    private int index;
    private int maxIndex;

    public PlatformBacknForth(FSM ownerFsm, GameObject platform, float speed, Transform[] path)
    {
        this.ownerFsm = ownerFsm;
        this.platform = platform;
        this.speed = speed;
        this.path = path;

        maxIndex = path.Length;
    }

    public void Enter()
    {
        index++;

        if (index >= maxIndex)
        {
            Array.Reverse(path);

            index = 0;
        }

    }

    public void Exit()
    {
    }

    public void StateUpdate()
    {
        float distanceToPoint = Vector2.Distance(platform.transform.position, path[index].position);

        if (distanceToPoint <= .25f)
        {
            ownerFsm.ChangeCurrentState(ownerFsm.GetState("PlatformPause"));
        }

        platform.transform.position = Vector2.MoveTowards(platform.transform.position, path[index].position, (Time.deltaTime * speed));

    }
}
