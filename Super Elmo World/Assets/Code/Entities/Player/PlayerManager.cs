﻿
using UnityEngine.InputSystem;
using UnityEngine;
using UnityEngine.InputSystem.Users;
using System;
using System.Collections.Generic;

public class PlayerManager : MonoBehaviour
{

    public int maximumPlayers;
    public Transform spawnPoint;
    public GameObject playerPrefab;
    public InputAction joinAction;
    public bool isJoiningAvailable;

    public int currentPlayerIndex { get; private set; }
    // Can pass these to the newly created players between scenes.
    private Dictionary<int, GameObject> players = new Dictionary<int, GameObject>();
    private DeviceEventArgs deviceEventArgs;

    public bool CanPlayerJoin { get { return (currentPlayerIndex < maximumPlayers); } }


    // Some events that anything can hear.
    public event EventHandler<DeviceEventArgs> OnDeviceConnected;
    public event EventHandler<DeviceEventArgs> OnDeviceDisconnected;

    public static event Action<int> OnPlayerJoined;

    private PlayerControls playerControls;

    private void Awake()
    {
        deviceEventArgs = new DeviceEventArgs();
        playerControls = new PlayerControls();
        
        //DontDestroyOnLoad(this.gameObject);
    }

    private void OnEnable()
    {
        InputSystem.onDeviceChange += OnDeviceChange;

        joinAction.performed += JoinPlayer;

        joinAction.Enable();
    }

    private void OnDisable()
    {
        InputSystem.onDeviceChange -= OnDeviceChange;

        joinAction.performed -= JoinPlayer;

        joinAction.Disable();
    }

    // Start is called before the first frame update
    void Start()
    {        
    }

    private void JoinPlayer(InputAction.CallbackContext context)
    {
        if (!CanPlayerJoin)
            return;
        
        InputDevice device = context.control.device;
        

        if (IsDeviceUsable(device, this.playerControls))
        {
            players.Add((currentPlayerIndex), SpawnPlayer(currentPlayerIndex, device));
            OnPlayerJoined?.Invoke(currentPlayerIndex);
            //InputUser newUser = new InputUser();
            //newUser = InputUser.PerformPairingWithDevice(device, newUser);
            //currentPlayerIndex++;
            //Debug.Log(InputUser.GetUnpairedInputDevices());


        }
        else
        {
            Debug.Log($"{device} is not a usable device.");
        }
        
    }

    public void DisconnectAllPlayers()
    {
        for (int i = 0; i < currentPlayerIndex; i++)
        {
            GameObject playerToRemove = players[i];            
         
            playerToRemove.GetComponent<PlayerInputHandler>().inputUser.UnpairDevicesAndRemoveUser();
            
            players.Remove(i);

            Destroy(playerToRemove);
        }

        currentPlayerIndex = 0;
    }

    // This will Spawn a player prefab, And then assign an available device to a newly created input user
    private GameObject SpawnPlayer(int playerIndex, InputDevice deviceToPair)
    {
        GameObject newPlayer = Instantiate(playerPrefab, spawnPoint.position, Quaternion.identity, this.transform);

        PlayerInputHandler newPlayerInputHandler = newPlayer.GetComponent<PlayerInputHandler>();
        newPlayerInputHandler.playerIndex = playerIndex;
        newPlayerInputHandler.deviceBeingUsed = deviceToPair;

        InputUser newInputUser;

        // Clear the list of devices for the player that are automatically registered.          
        newPlayerInputHandler.playerControls.devices = new InputDevice[] { };          
        newInputUser = InputUser.PerformPairingWithDevice(deviceToPair);        
        newInputUser.AssociateActionsWithUser(newPlayerInputHandler.playerControls.asset);

        newPlayerInputHandler.inputUser = newInputUser;

        currentPlayerIndex++;

        return newPlayer;
    }



    private bool IsDeviceUsable(InputDevice device, PlayerControls playerControls)
    {
        InputActionAsset actions = playerControls.asset;

        if (actions.controlSchemes.Count > 0)
        {
            if (!(InputUser.GetUnpairedInputDevices().Contains(device)))
                return false;
                
            if (InputControlScheme.FindControlSchemeForDevice(device, actions.controlSchemes) == null)    
                return false;
           

            return true;
        }

        // This is so if there are no controlSchemes, it checks whether the device can be used with the actionMap.
        foreach (var actionMap in actions.actionMaps)
            if (actionMap.IsUsableWithDevice(device))
                return true;

        return false;
    }


    private void OnDeviceChange(InputDevice device, InputDeviceChange inputDeviceChange)
    {
        deviceEventArgs.device = device;

        switch (inputDeviceChange)
        {
            case InputDeviceChange.Added:
                Debug.Log($"{device} was added.");
                OnDeviceConnected?.Invoke(this, deviceEventArgs);
                break;
            case InputDeviceChange.Removed:
                Debug.Log($"{device} was removed.");
                OnDeviceDisconnected?.Invoke(this, deviceEventArgs);
                break;
            default:
                break;
        }

    }

}

public class DeviceEventArgs : EventArgs
{
    public InputDevice device;

    public DeviceEventArgs()
    { 
        
    }

}