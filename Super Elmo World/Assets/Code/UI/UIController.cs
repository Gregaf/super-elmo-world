using System.Net.NetworkInformation;
using System.Text;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    public Canvas[] screens;
    private Canvas currentScreen;
    private Canvas previousScreen;
    
    public bool isPaused = false;
    public TextMeshProUGUI timerText;

    private void OnEnable()
    {
        GameState gameState = (GameState) GameManager.Instance.GameStateManager.GetState("Game");

        gameState.updateTimeCallback += UpdateText;
    }

    private void OnDisable()
    {
        GameState gameState = (GameState)GameManager.Instance.GameStateManager.GetState("Game");

        gameState.updateTimeCallback -= UpdateText;
    }

    private void Start()
    {
        currentScreen = screens[0];
    }

    public void PauseGame()
    {
        
        if (!isPaused)
        {
            isPaused = true;
            Time.timeScale = 0;
            screens[0].gameObject.SetActive(false);
            screens[1].gameObject.SetActive(true);
        }
        else
        {
            isPaused = false;
            Time.timeScale = 1;
            screens[0].gameObject.SetActive(true);
            screens[1].gameObject.SetActive(false);

        }

        // Maybe first check whether the game is already paused. so do opposite if the game is already paused.
        // Game timescale will be set to 0 or 1
        // Set Game UI canvas to inactive
        // Then enable the Pause Menu Canvas
    }

    public void UpdateText(float levelTimer)
    {
        int displayedTime = Mathf.RoundToInt(levelTimer);

        timerText.text = $"Time: {displayedTime}";
    }

}
