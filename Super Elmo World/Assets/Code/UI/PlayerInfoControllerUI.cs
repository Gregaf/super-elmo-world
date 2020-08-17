using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInfoControllerUI : MonoBehaviour
{
    [SerializeField] private GameObject[] playerPanels;

    private int maxPlayers = 4;

    private void Awake() 
    {
        playerPanels = new GameObject[maxPlayers];

        for(int i = 0; i < maxPlayers; i++)
        {
            playerPanels[i] = transform.GetChild(i).gameObject;

        }

    }
    private void OnEnable()
    {
        PlayerManager.OnPlayerJoined += EnablePlayerPanel;
    }

    private void OnDisable()
    {
        PlayerManager.OnPlayerJoined -= EnablePlayerPanel;
    }

    private void EnablePlayerPanel(int currentIndex)
    {
        playerPanels[(currentIndex - 1)].SetActive(true);

    }

}
