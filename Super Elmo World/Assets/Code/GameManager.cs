using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance { get; private set; }

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

    }

    public void Start()
    {
        // There should only be one instance of LevelData per scene.
        currentLevel = FindObjectOfType<LevelData>();
        levelTimer = currentLevel.timeToComplete;

    }

    private void Update()
    {
        levelTimer -= Time.deltaTime;


        updateTimeText.Invoke(levelTimer);
    }

}
