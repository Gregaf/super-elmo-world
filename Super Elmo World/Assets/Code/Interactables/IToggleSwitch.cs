using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IToggleSwitch
{
    event Action<bool> OnSwitchActuate;

}
