using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayerProfile : MonoBehaviour
{
    public string playerName;
    public int currency;

    public void Increment()
    {
        currency++;
        SerializationManager.Save("Profile", currency);

    }

    public void OnNew()
    {


    }

    public void OnLoad()
    {

    }

}
