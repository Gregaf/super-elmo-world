using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneSwitcher : MonoBehaviour
{
    private Scene currentActiveScene;
    // Will have a seperate function to offer ability to pass in scene to change
    private Scene worldMapScene;
    private const int worldSceneIndex = 2;
    [SerializeField] private bool currentlySwitching = false;

    public event Action OnSwitchScene;

    void Start()
    {
        currentActiveScene = SceneManager.GetActiveScene();
        Debug.Log(currentActiveScene.buildIndex);
    }

    public void ChangeToWorldMap()
    {
        if(!currentlySwitching)
            StartCoroutine(LoadNewScene(worldSceneIndex));

        currentlySwitching = true;
    }


    IEnumerator LoadNewScene(int newSceneIndex)
    {
        AsyncOperation asyncNewSceneLoad = SceneManager.LoadSceneAsync(newSceneIndex, LoadSceneMode.Additive);
        yield return asyncNewSceneLoad;
        SceneManager.UnloadSceneAsync(1);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<PlayerController>() != null)
        {
            ChangeToWorldMap();
        }

    }

}
