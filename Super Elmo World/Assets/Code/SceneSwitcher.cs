using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using WorldMap;

public class SceneSwitcher : MonoBehaviour
{
    private int currentActiveSceneIndex;
    // Will have a seperate function to offer ability to pass in scene to change
    private const int worldSceneIndex = 1;
    [SerializeField] private bool currentlySwitching = false;

    void Start()
    {
        currentActiveSceneIndex = SceneManager.GetActiveScene().buildIndex;

    }

    private void OnEnable()
    {
        Level.OnLevelChosen += ChangeToSpecifiedScene;
    }

    private void OnDisable()
    {
        Level.OnLevelChosen -= ChangeToSpecifiedScene;
    }

    public void ChangeToWorldMap()
    {
        if(!currentlySwitching)
            StartCoroutine(LoadNewScene((int) SceneIndexes.OVERWORLD));
    }

    /// <summary>
    /// Load the specifed scene at asynchronously with 'Single' load mode.
    /// </summary>
    /// <param name="newSceneIndex"></param>
    public void ChangeToSpecifiedScene(SceneIndexes newSceneIndex)
    {
        if (!currentlySwitching)
            StartCoroutine(LoadNewScene((int) newSceneIndex));
    }

    IEnumerator LoadNewScene(int newSceneIndex)
    {
        currentlySwitching = true;

        AsyncOperation asyncNewSceneLoad = SceneManager.LoadSceneAsync(newSceneIndex, LoadSceneMode.Single);
        asyncNewSceneLoad.allowSceneActivation = false;

        while (!asyncNewSceneLoad.isDone)
        {
            if (Input.GetKeyDown(KeyCode.N))
            {
                asyncNewSceneLoad.allowSceneActivation = true;
            }
            

            yield return null;
        }


        currentlySwitching = false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<PlayerController>() != null)
        {
            ChangeToWorldMap();
        }

    }

}
