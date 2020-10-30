using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{

    [SerializeField] private float levelTime;
    [SerializeField] private AudioClip levelMusic;
    [SerializeField] private Transform currentCheckpoint;

    private void Start()
    {
        AudioManager.Instance.PlayTrack(levelMusic);

    }


    IEnumerator RestartLevel()
    {
        // Fade to reset screen
        // Reintialize players
        // Reposition the players
        

        // Thoughts: Possibly could reload the scene if I just save the state of everything such as checkpoints. Reset lives of players that are perma dead.

        yield return null;
    }

}
