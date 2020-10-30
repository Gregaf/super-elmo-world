using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.Events;

[CreateAssetMenu(fileName = "Item", menuName = "ScriptableObjects/NewItem", order = 1)]
public class Item : ScriptableObject
{
    public string itemName;
    public string description;
    
    public int cost;


}
