using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    [SerializeField] private AudioClip levelMusic;

    private void Start()
    {
        AudioManager.Instance.PlayTrack(levelMusic);

    }

}
