using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIController : MonoBehaviour
{
    public GameObject currentMenu;
    public GameObject[] seperateMenus;

    private GameObject previousMenu;
    private int numberOfMenus;

    protected virtual void Start()
    {
        numberOfMenus = seperateMenus.Length;
        currentMenu = seperateMenus[0];
    }

    public void SetMenu(int index)
    {
        if (index >= numberOfMenus)
            return;

        previousMenu = currentMenu;
        currentMenu = seperateMenus[index];

        SetCurrentActive();
    }

    public void ReturnToPrevious()
    {
        GameObject temp = currentMenu;
        currentMenu = previousMenu;
        previousMenu = temp;

        SetCurrentActive();
    }

    private void SetCurrentActive()
    {
        previousMenu.SetActive(false);
        currentMenu.SetActive(true);
    }

}
