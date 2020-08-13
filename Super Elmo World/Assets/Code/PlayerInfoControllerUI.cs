using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInfoControllerUI : MonoBehaviour
{
    [SerializeField] private GameObject[] playerPanels;

    private void Awake() 
    {
        playerPanels = new GameObject[4];

        for(int i = 0; i < 4; i++)
        {
            playerPanels[i] = transform.GetChild(i).gameObject;

        }

    }

    private void Start() 
    {
        PlayerManager.Instance.OnPlayerJoined += EnableRespectivePanel;
    }
    private void OnDisable() 
    {
        PlayerManager.Instance.OnPlayerJoined -= EnableRespectivePanel;
    }

    private void EnableRespectivePanel()
    {
        int playerIndex = PlayerManager.Instance.currentPlayerIndex - 1;

        playerPanels[playerIndex].SetActive(true);
    }

}
