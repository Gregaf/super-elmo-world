using System;
using UnityEngine;

public class GrowthState : MonoBehaviour
{
    private PlayerBrain playerBrain;
    private HealthScript playerHealth;
    private FSM growthFsm;
    private PickUp pickUp;

    void Awake()
    {
        playerBrain = this.GetComponent<PlayerBrain>();
        playerHealth = this.GetComponent<HealthScript>();
        growthFsm = new FSM();

        growthFsm.AddToStateList("Small", new Small(playerBrain));
        growthFsm.AddToStateList("Big", new Big(playerBrain));

    }

    private void OnEnable()
    {
        playerHealth.OnLoseHealthCallback += ChangeState;

    }

    private void OnDisable()
    {
        playerHealth.OnLoseHealthCallback -= ChangeState;
    }

    private void Start()
    {
        growthFsm.InitializeFSM(growthFsm.GetState("Big"));

    }

    private void OnTriggerEnter2D(Collider2D collider2D)
    {
        if (collider2D.GetComponent<PickUp>() != null)
        {
            pickUp = collider2D.GetComponent<PickUp>();

            pickUp.pickUpEventHandler += ChangeState;

        }
    }

    private void OnTriggerExit2D(Collider2D collider2D)
    {

    }

    public void ChangeState(System.Object sender, PickUpEventArgs e)
    {

        //switch (e.growthState)
        //{
        //    case GrowthStates.Small:
        //        growthFsm.ChangeCurrentState("Small");
        //        break;
        //    case GrowthStates.Big:
        //        growthFsm.ChangeCurrentState("Big");
        //        break;
        //    default:
        //        throw new Exception("FUUUUCK");
        //}

        //if (pickUp?.pickUpEventHandler != null)
        //{
        //    pickUp.pickUpEventHandler -= ChangeState;
        //}

    }


}
