
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance { get; private set; }
    public FSM GameStateManager;

    [SerializeField]
    private LevelData currentLevel;

    public float levelTimer { get; private set; }
    public float score { get; private set; }
    public float currentCoins { get; private set; }

    public delegate void UpdateTimeText(float time);
    public static UpdateTimeText updateTimeText;



    public void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Debug.Log("There is already an instance of " + this);
        }


        GameStateManager = new FSM();

        GameStateManager.AddToStateList("Paused", new GM_Paused(GameStateManager));
        GameStateManager.AddToStateList("World", new GM_WorldSelect(GameStateManager));
        GameStateManager.AddToStateList("Game", new GM_InGame(GameStateManager));
    }

    public void Start()
    {
        GameStateManager.InitializeFSM(GameStateManager.GetState("Game"));

        currentLevel = FindObjectOfType<LevelData>();
        levelTimer = currentLevel.timeToComplete;

    }

    private void Update()
    {
        levelTimer -= Time.deltaTime;


        updateTimeText.Invoke(levelTimer);
    }

}
