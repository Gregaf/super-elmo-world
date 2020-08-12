using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelMarker : MonoBehaviour
{
    [SerializeField] private string levelName;
    [SerializeField] private string levelDescription;
    [SerializeField] private string sceneToLoad;
    private bool isLocked = true;
    public Transform[] paths;

    public void Awake()
    {
    }

    public void UnlockLevel()
    {
        this.isLocked = false;
    }

}
