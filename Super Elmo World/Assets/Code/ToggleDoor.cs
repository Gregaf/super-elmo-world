using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToggleDoor : Door
{
    public IToggleSwitch toggleSwitch;

    protected override void Awake()
    {
        base.Awake();
        toggleSwitch = this.GetComponentInChildren<IToggleSwitch>();
    }

    private void OnEnable()
    {
        toggleSwitch.OnSwitchActuate += ChangeDoorState;
    }

    private void OnDisable()
    {
        toggleSwitch.OnSwitchActuate -= ChangeDoorState;
    }

    private void ChangeDoorState(bool isActive)
    {
        if (isActive)
            Open();
        else
            Close();
    }
}
