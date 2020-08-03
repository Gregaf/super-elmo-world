using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public abstract class Entity : MonoBehaviour
{
    protected HealthScript healthManager;
    [SerializeField] private bool usesPhysics;
    protected CharacterController2D physicsController;
    [SerializeField] protected int entityID;
    protected bool isFacingRight;

    protected string entityName { get; private set; }
    protected string entityDescription { get; private set; }

    protected virtual void Awake()
    {
        healthManager = this.GetComponent<HealthScript>();

        if (usesPhysics)
            physicsController = this.GetComponent<CharacterController2D>();

        ReadDescriptionFile();

        // If the localScale is positive then the entity is facing to the right.
        isFacingRight = transform.localScale.x > 0;

    }

    protected virtual void OnEnable()
    { 
    
    }
    protected virtual void OnDisable()
    {

    }

    protected virtual void Start()
    {
        print(this.ToString());
    }

    protected virtual void OnTriggerEnter2D(Collider2D collider)
    {
        

    }

    private void ReadDescriptionFile()
    {
        string filePath = "Assets/EntityDescriptions/EntityID" + entityID + ".txt";

        StreamReader streamReader = new StreamReader(filePath);

        if (streamReader == null)
            return;
        entityName = streamReader.ReadLine();
        entityDescription = streamReader.ReadToEnd();

        streamReader.Close();
    }

    public override string ToString()
    {
        return ("Name: " + entityName + "\n" + "Description: " + entityDescription);
    }
}