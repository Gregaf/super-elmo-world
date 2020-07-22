using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Threading;
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
        PlayerInput.onPause += PauseGame;
        GameManager.updateTimeText += UpdateText;
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

        timerText.text = "Time: " + displayedTime;
    }

    private void OnDisable()
    {
        PlayerInput.onPause -= PauseGame;
        GameManager.updateTimeText -= UpdateText;
    }

}
