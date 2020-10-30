using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class ItemPanel : MonoBehaviour
{
    // All thbe refrences n stuff.
    private Button button;
    public Item data;
    public UnityEvent OnUsed;


    private void Start()
    {
        button = GetComponent<Button>();
        button.onClick.AddListener(OnUse);


    }


    private void OnDisable()
    {
        button.onClick.RemoveListener(OnUse);
    }

    private void OnUse()
    {
        int numberOfEvents = button.onClick.GetPersistentEventCount();

        if (data.cost <= Storage.currency)
        {
            Debug.Log("<color=green>SUCCESSFULLY PURCHASED!</color>");
            Storage.currency -= data.cost;
            OnUsed.Invoke();
        }
        else
        {
            Debug.Log("<color=red>YOU CAN'T AFFORD THIS!</color>");
        }

        
    }

}
