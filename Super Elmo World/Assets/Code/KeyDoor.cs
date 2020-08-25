using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyDoor : Door
{
    public int numberOfKeysRequired;
    public int currentNumberOfKeys;
    

    protected override void Awake()
    {
        base.Awake();

    }

    protected override void Start()
    {
        base.Start();

    }

    public void KeyBrought()
    {
        currentNumberOfKeys++;

        if (currentNumberOfKeys >= numberOfKeysRequired)
        {
            Open();
        }
    }



}
