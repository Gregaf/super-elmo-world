using System;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameState gameState { get; private set; }
    public static GameManager Instance { get { return instance; } }
    private static GameManager instance;

    public FSM GameStateManager { get; private set; }
    public PlayerData[] eachPlayersData;

    [SerializeField]
    private LevelData currentLevel;

    public float levelTimer { get; private set; }

    public void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
            Debug.LogAssertion("There is already an instance of " + this.ToString());

        eachPlayersData = new PlayerData[4];

        for (int i = 0; i < 4; i++)
        {
            PlayerData newPlayer = new PlayerData();

            eachPlayersData[i] = newPlayer;
        }

        currentLevel = FindObjectOfType<LevelData>();
        levelTimer = currentLevel.levelTime;

        GameStateManager = new FSM();

        GameStateManager.AddToStateList("Game", new GameState(GameStateManager, levelTimer));
        GameStateManager.AddToStateList("World", new GM_WorldSelect());

        GameStateManager.InitializeFSM(GameStateManager.GetState("Game"));
    }

    private void OnDisable()
    {
    }

    public void Start()
    {
    }

    private void Update()
    {
        GameStateManager.UpdateCurrentState();

        if (Input.GetKeyDown(KeyCode.W))
            GameStateManager.ChangeCurrentState("World");

        if (Input.GetKeyDown(KeyCode.G))
            GameStateManager.ChangeCurrentState("Game");

    }

    public void PlayerDataUpdated(System.Object o, EventArgs e)
    { 
        
    }

}
