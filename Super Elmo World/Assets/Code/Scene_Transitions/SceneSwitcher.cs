using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneSwitcher : MonoBehaviour
{
    [SerializeField] private int currentActiveSceneIndex;
    // Will have a seperate function to offer ability to pass in scene to change
    private const int worldSceneIndex = 1;
    [SerializeField] private bool currentlySwitching = false;

    bool load = false;

    public static event Action<int> OnSceneLoaded;

    void Start()
    {
        currentActiveSceneIndex = SceneManager.GetActiveScene().buildIndex;
        
    }

    private void OnEnable()
    {

    }

    private void OnDisable()
    {

    }

    public void ChangeToWorldMap()
    {
        if(!currentlySwitching)
            StartCoroutine(LoadNewScene((int) SceneIndexes.OVERWORLD));
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.C))
        {
            print("Load!");
            ChangeToSpecifiedScene(SceneIndexes.LEVEL1);
        }
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

        WaitForEndOfFrame waitForEndOfFrame = new WaitForEndOfFrame();

        AsyncOperation asyncNewSceneLoad = SceneManager.LoadSceneAsync(newSceneIndex, LoadSceneMode.Additive);
        asyncNewSceneLoad.allowSceneActivation = false;

        if(OnSceneLoaded != null)
            OnSceneLoaded(newSceneIndex);

        StartCoroutine(PollInput());

        while (!asyncNewSceneLoad.isDone)
        {
            if (load)
            {
                asyncNewSceneLoad.allowSceneActivation = true;
            }
            

            yield return waitForEndOfFrame;
        }

        AsyncOperation asyncUnloadPrevious = SceneManager.UnloadSceneAsync(currentActiveSceneIndex);

        currentActiveSceneIndex = newSceneIndex;

        currentlySwitching = false;
    }

    IEnumerator PollInput()
    {
        load = false;

        while (!Input.GetKeyDown(KeyCode.N))
        {

            yield return null;
        }

        load = true;

        yield return null;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {

    }

}
