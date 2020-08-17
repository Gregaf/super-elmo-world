using System;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get { return instance; } }
    private static GameManager instance;

    private List<PlayerBrain> activePlayers;


    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
            Debug.LogAssertion("There is already an instance of " + this.ToString());


        activePlayers = new List<PlayerBrain>();
    }

    private void OnEnable()
    {
        PlayerManager.OnPlayerJoined += UpdateActivePlayers;

    }

    private void OnDisable()
    {
        PlayerManager.OnPlayerJoined -= UpdateActivePlayers;

    }

    private void Start()
    {

    }

    private void Update()
    {

    }

    private void UpdateActivePlayers(int numberOfPlayers)
    {
        PlayerBrain[] players = FindObjectsOfType<PlayerBrain>();

        activePlayers.Clear();

        activePlayers.AddRange(players);

    }

    public List<PlayerBrain> GetActivePlayers()
    {
        return this.activePlayers;
    }

}

