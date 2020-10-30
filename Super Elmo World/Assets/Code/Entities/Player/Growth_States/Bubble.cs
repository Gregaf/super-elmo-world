#undef DEBUG


using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;

public class Bubble : State
{
    PlayerController playerController;

    private List<Transform> targetPositions;
    //private Transform[] targetPositions;
    private Transform target;

    private Vector2 store;

    public static event Action<PlayerData> OnPlayerRevive;

    public Bubble(PlayerController playerController)
    {
        this.playerController = playerController;
    }

    public override void Enter()
    {       
        playerController.velocity = Vector2.zero;

        targetPositions = GameManager.Instance.Game_State.GetLivingPlayers();
        Debug.Log(targetPositions.Count);
        

        target = GetClosestPlayer(targetPositions);

        Vector2 newPos = new Vector2(target.position.x, target.position.y + 30f);

        playerController.transform.position = newPos;
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
        target = GetClosestPlayer(targetPositions);

#if DEBUG
        Debug.Log($"<color=cyan>{playerController.gameObject}</color> is tracking to <color=yellow>{target.gameObject}</color> at position <b>{target.position}</b>.");
#endif

        playerController.transform.position = Vector2.MoveTowards(playerController.transform.position, target.position, 5 * Time.deltaTime);
    }


    private Transform GetClosestPlayer(List<Transform> players)
    {
        Transform closestTarget = players[0];

        for (int i = 0; i < players.Count; i++)
        {
            if (Vector2.Distance(playerController.transform.position, players[i].position) < Vector2.Distance(playerController.transform.position, closestTarget.position))
                closestTarget = players[i];
        }


        return closestTarget;
    }

}
