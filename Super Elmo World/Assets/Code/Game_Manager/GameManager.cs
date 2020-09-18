using System;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;
using BaseGame;


public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get { return instance; } }
    private static GameManager instance;


    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
            Debug.LogAssertion("There is already an instance of " + this.ToString());


    }

    private void OnEnable()
    {

    }

    private void OnDisable()
    {

    }

    private void Start()
    {

    }

    private void Update()
    {

    }

}

