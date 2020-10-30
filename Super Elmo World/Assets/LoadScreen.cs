using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class LoadScreen : MonoBehaviour
{
    public GameObject backgroundGO;
    public Image backgroundImage;

    private void Start()
    {
        backgroundImage.CrossFadeAlpha(0f, 0.01f, false);
    }

    void OnEnable()
    {
        SceneSwitcher.OnSceneLoaded += FadeScreenIn;
    }

    // Update is called once per frame
    void OnDisable()
    {
        SceneSwitcher.OnSceneLoaded -= FadeScreenIn;
    }


    private void FadeScreenIn(int sceneIndex)
    {
        print("HEY!");
        backgroundImage.CrossFadeAlpha(1f, 0.5f, false);

    }



}
