using System;
using UnityEngine;

public class GrowthState : MonoBehaviour
{
    private PlayerBrain playerBrain;
    private HealthManager healthManager;
    private FSM growthFsm;

    void Awake()
    {
        playerBrain = this.GetComponent<PlayerBrain>();

        growthFsm = new FSM();
        healthManager = this.GetComponent<HealthManager>();

        growthFsm.AddToStateList("Small", new Small(playerBrain));
        growthFsm.AddToStateList("Big", new Big(playerBrain));
        growthFsm.AddToStateList("Glide", new Gliding(playerBrain));

    }

    private void OnEnable()
    {
        healthManager.OnHealthChange += ChangeState;
    }

    private void OnDisable()
    {
        healthManager.OnHealthChange -= ChangeState;
    }

    private void Start()
    {
        growthFsm.InitializeFSM(growthFsm.GetState("Small"));

    }

    private void OnTriggerEnter2D(Collider2D collider2D)
    {

    }

    private void OnTriggerExit2D(Collider2D collider2D)
    {

    }

    public void ChangeState(System.Object o, GrowthEventArgs e)
    {

        switch (e.currentHealth)
        {
            case 1:
                growthFsm.ChangeCurrentState("Small");
                break;
            case 2:
                growthFsm.ChangeCurrentState("Big");
                break;
            case 3:
                SubStates(e.growthID);
                break;
            default:
                Debug.LogError("CurrentHealth is out of bounds.");
                break;
        }

    }

    private void SubStates(int itemID)
    {
        switch (itemID)
        {
            case 0:
                // growthFsm.ChangeCurrentState("Projectile");
                Debug.Log("Change to Fire State");
                break;
            case 1:
                growthFsm.ChangeCurrentState("Glide");
                Debug.Log("Change to Cape State");
                break;
            case 2:
                // growthFsm.ChangeCurrentState("Some state");
                break;
        }
    
    }

}
