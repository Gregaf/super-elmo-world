using System.IO;
using UnityEngine;

public abstract class Entity : MonoBehaviour
{
    public bool isFacingRight { get; protected set; }

    [SerializeField] private bool usesPhysics = true;

    protected CharacterController2D physicsController;
    [SerializeField] protected int entityID;
    protected string entityName { get; private set; }
    protected string entityDescription { get; private set; }

    protected virtual void Awake()
    {
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

    // Reads the name and description from a text file that corresponds to the EntityID and writes to entityName and entityDescription.
    private void ReadDescriptionFile()
    {
        string filePath = "Assets/EntityDescriptions/EntityID" + entityID + ".txt";

        // Throws fileNotFoundException if file doesn't Exist or path is incorrect.
        StreamReader streamReader = new StreamReader(filePath);
        
        entityName = streamReader.ReadLine();
        entityDescription = streamReader.ReadToEnd();

        streamReader.Close();
    }

    public override string ToString()
    {
        return ($"Name: {entityName}\n Description{entityDescription}");
    }
}