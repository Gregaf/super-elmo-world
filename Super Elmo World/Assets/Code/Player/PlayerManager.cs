
using UnityEngine.InputSystem;
using UnityEngine;
using UnityEngine.InputSystem.Users;
using System;


public class PlayerManager : MonoBehaviour
{
    public static PlayerManager Instance { get; private set; }

    public int maximumPlayers;
    public Transform spawnPoint;
    public GameObject playerPrefab;
    public InputAction joinAction;
    PlayerControls playerControls;

    [SerializeField] private int currentPlayerIndex;
    // Can pass these to the newly created players between scenes.
    private PlayerInputHandler[] players;
    private DeviceEventArgs deviceEventArgs;

    public bool CanPlayerJoin { get { return (currentPlayerIndex < maximumPlayers); } }


    // Some events that anything can hear.
    public static event EventHandler<DeviceEventArgs> OnDeviceConnected;
    public static event EventHandler<DeviceEventArgs> OnDeviceDisconnected;
    public static event Action OnPlayerJoined;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Debug.LogError($"There is already an instace of {this}.");

        deviceEventArgs = new DeviceEventArgs();
        playerControls = new PlayerControls();
        players = new PlayerInputHandler[maximumPlayers];

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
            SpawnPlayer(currentPlayerIndex, device);
            OnPlayerJoined?.Invoke();
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

    // This will Spawn a player prefab, And then assign an available device to a newly created input user
    private void SpawnPlayer(int playerIndex, InputDevice deviceToPair)
    {
        GameObject newPlayer = Instantiate(playerPrefab, spawnPoint.position, Quaternion.identity);
        PlayerInputHandler newPlayerInputHandler = newPlayer.GetComponent<PlayerInputHandler>();
        newPlayerInputHandler.playerIndex = playerIndex;
        InputUser newInputUser = newPlayerInputHandler.inputUser;
        
        // Clear the list of devices for the player that are automatically registered.          
        newPlayerInputHandler.playerControls.devices = new InputDevice[] { };          
        newInputUser = InputUser.PerformPairingWithDevice(deviceToPair, newInputUser);        
        newInputUser.AssociateActionsWithUser(newPlayerInputHandler.playerControls.asset);
                   
        currentPlayerIndex++;     
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