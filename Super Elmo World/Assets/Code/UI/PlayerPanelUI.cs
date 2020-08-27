using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerPanelUI : MonoBehaviour
{
    public Image[] playerJoinImage = new Image[4];


    private void Awake()
    {
        
    }

    private void Start()
    {
        for (int i = 0; i < playerJoinImage.Length; i++)
            playerJoinImage[i].enabled = false;
    }

    private void OnEnable()
    {
        PlayerManager.OnPlayerJoined += ShowPlayer;
    }

    private void OnDisable()
    {
        PlayerManager.OnPlayerJoined -= ShowPlayer;
    }


    private void ShowPlayer(int playerIndex) 
    {
        playerJoinImage[playerIndex - 1].enabled = true;
    }
}
