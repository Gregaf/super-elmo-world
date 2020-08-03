using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameState gameState { get; private set; }
    public static GameManager Instance { get { return instance; } }
    private static GameManager instance;

    public FSM GameStateManager { get; private set; }

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

}
