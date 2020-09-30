using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToggleSwitch : MonoBehaviour, IToggleSwitch
{
    public bool isOn { get; private set; }

    public event Action<bool> OnSwitchActuate;

    private void OnTriggerEnter2D(Collider2D collider2D)
    {
        if (collider2D.GetComponent<Entity>() != null)
        {
            isOn = !isOn;
            OnSwitchActuate?.Invoke(isOn);
        }
    }
}
