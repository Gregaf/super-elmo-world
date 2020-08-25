using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainMenuManager : MonoBehaviour
{
    public Canvas[] screens;

    private Canvas currentScreen;
    private Canvas previousScreen;
    Button button;

    private void Start()
    {
        currentScreen = screens[0];

    }

    public void StartGame()
    {
        Debug.Log("Start the game.");
    }
    public void GoToOptions()
    {
        Debug.Log("Selected Options.");
        currentScreen.gameObject.SetActive(false);

        previousScreen = currentScreen;

        currentScreen = screens[1];

        currentScreen.gameObject.SetActive(true);
    }

    public void ReturnToPrevious()
    {
        Canvas temp = currentScreen;

        temp.gameObject.SetActive(false);

        currentScreen = previousScreen;
        previousScreen = temp;

        currentScreen.gameObject.SetActive(true);
    }

    public void QuitGame()
    {
        Debug.Log("Quit game.");
    }



}
