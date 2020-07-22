using System.Collections;
using UnityEngine;

public class PlatformCycle : State
{

    private GameObject platform;
    private float speed;
    private Transform[] path;

    private int index;
    private int maxIndex;

    public PlatformCycle(FSM ownerFsm, GameObject platform, float speed, Transform[] path)
    {
        this.ownerFsm = ownerFsm;
        this.platform = platform;
        this.speed = speed;
        this.path = path;

        maxIndex = path.Length;
    }

    public override void Enter()
    {
        index++;

        if (index >= maxIndex)
        {
            index = 0;
        }
    }

    public override void Exit()
    {

    }

    public override void StateUpdate()
    {
        
        float distanceToPoint = Vector2.Distance(platform.transform.position, path[index].position);

        if (distanceToPoint <= .25f)
        {
            ownerFsm.ChangeCurrentState(ownerFsm.GetState("PlatformPause"));
        }

        platform.transform.position = Vector2.MoveTowards(platform.transform.position, path[index].position, (Time.deltaTime * speed));
    }

    
}
