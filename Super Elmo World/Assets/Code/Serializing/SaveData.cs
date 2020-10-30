using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SaveData : MonoBehaviour
{
    private static SaveData _current;
    public static SaveData Current { get { return _current; } }

    public PlayerProfile playerProfile;


}
