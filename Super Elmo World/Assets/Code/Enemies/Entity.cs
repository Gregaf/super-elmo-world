using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Entity : MonoBehaviour
{
    protected HealthScript healthManager;
    protected CharacterController2D physicsController;
    protected int entityID;

    protected void Start()
    {
        
    }

    // Every entity has a death function that must be inherited
    public virtual void Die()
    { 
        
    }
}
