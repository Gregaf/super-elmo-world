using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameState : State
{
    private int currentPlayerCount = 1;

    List<Transform> livingPlayersPositions = new List<Transform>();
    List<PlayerData> livingPlayers = new List<PlayerData>();
    List<PlayerData> deadPlayers = new List<PlayerData>();

    public GameState(PlayerData[] players, int currentPlayerCount)
    {
        livingPlayers.AddRange(players);

        for (int i = 0; i < currentPlayerCount; i++)
        {
            livingPlayersPositions.Add(players[i].transform);
        }

        this.currentPlayerCount = currentPlayerCount;
    }

    public override void Enter()
    {
        Dead.OnPlayerDied += AddToDead;

    }

    public override void Exit()
    {
        Dead.OnPlayerDied -= AddToDead;
    }

    public override void OnTriggerEnter2D(Collider2D collider2D)
    {
        throw new System.NotImplementedException();
    }

    public override void Tick()
    {
    }

    public List<Transform> GetLivingPlayers()
    {


        return livingPlayersPositions;
    }


    private void AddToDead(PlayerData newDeadPlayer)
    {
        if (!deadPlayers.Contains(newDeadPlayer))
        {
            livingPlayersPositions.Remove(newDeadPlayer.transform);
            livingPlayers.Remove(newDeadPlayer);

            deadPlayers.Add(newDeadPlayer);
        }


        if (currentPlayerCount == deadPlayers.Count)
        {
            // All the players have died.
            GameOver();
        }
    }

    private void AddToLiving(PlayerData newLivingPlayer)
    {
        if (!livingPlayers.Contains(newLivingPlayer))
        {
            deadPlayers.Remove(newLivingPlayer);

            livingPlayersPositions.Add(newLivingPlayer.transform);
            livingPlayers.Add(newLivingPlayer);
        }
        
    }

    private void GameOver()
    {
        Debug.Log("<color=red>GAME OVER!</color>");
    }

}
