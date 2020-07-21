using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthScript : MonoBehaviour
{
    // Start is called before the first frame update
    int maxHealth = 3;
    int currentHealth = 1;
    public void addHealth(int amt)
    {
        //picking up power up
        if(currentHealth < maxHealth){
        currentHealth = currentHealth + amt;
            //checks that it doesnt go above max
            if(currentHealth > maxHealth)
            {
             currentHealth = maxHealth;
            }
        }
        //if already maxed
        else{
            Debug.Log("Health already max");
        }
    }

    public void subHealth(int amt)
        {
            //taking damage
            currentHealth = currentHealth - amt;
        }
    // Update is called once per frame
    void Update()
    {
        
    }
}
