using System;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get { return instance; } }
    private static GameManager instance;

    private FSM GameStateFSM;
    public GameState Game_State { get; private set; }

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
            Debug.LogAssertion("There is already an instance of " + this.ToString());

        PlayerData[] players = FindObjectsOfType<PlayerData>();

        GameStateFSM = new FSM();
        Game_State = new GameState(players, players.Length);

        GameStateFSM.InitializeFSM(Game_State);
    }

    private void OnEnable()
    {

    }

    private void OnDisable()
    {

    }

    private void Start()
    {

    }

    private void Update()
    {

    }

}

