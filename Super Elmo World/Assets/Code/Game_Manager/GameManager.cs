using System;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;
using BaseGame;


public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get { return instance; } }
    private static GameManager instance;

    public List<PlayerController> activePlayers;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
            Debug.LogAssertion("There is already an instance of " + this.ToString());


        activePlayers = new List<PlayerController>();
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
        PlayerController[] players = FindObjectsOfType<PlayerController>();

        activePlayers.Clear();

        activePlayers.AddRange(players);
    }

    public List<PlayerController> GetActivePlayers()
    {
        return this.activePlayers;
    }

}

