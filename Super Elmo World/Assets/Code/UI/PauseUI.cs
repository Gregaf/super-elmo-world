using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PauseUI : UIController
{
    public bool isPaused = false;

    private PlayerInputHandler playerInput = null;

    public event Action OnPauseGame;

    protected override void Start()
    {
        base.Start();
    }

    // Prototype
    public void TogglePause(InputAction.CallbackContext context)
    {
        playerInput = FindObjectOfType<PlayerInputHandler>();
        

        isPaused = !isPaused;

        if (isPaused)
        {
            Unpause();
        }
        else
            Pause();
    }

    private void Unpause()
    {
        playerInput.playerControls.Basic.Enable();
        Time.timeScale = 1;
        this.SetMenu(0);
    }

    private void Pause()
    {
        playerInput.playerControls.Basic.Disable();
        OnPauseGame?.Invoke();
        Time.timeScale = 0;
        this.SetMenu(1);
    }

}
