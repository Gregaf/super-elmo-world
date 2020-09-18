using System.IO;
using UnityEngine;

public abstract class Entity : MonoBehaviour
{
    public Vector2 velocity;
    public bool isFacingRight { get; protected set; }

    public Controller2D controller2D {get; private set;}
    [SerializeField] protected int entityID;
    public BoxCollider2D entityCollider { get; private set; }
    public string entityName { get; private set; }
    public string entityDescription { get; private set; }

    protected virtual void Awake()
    {

        controller2D = this.GetComponent<Controller2D>();
        entityCollider = this.GetComponent<BoxCollider2D>();

        ReadDescriptionFile();

        // If the localScale is positive then the entity is facing to the right.
        isFacingRight = transform.localScale.x > 0;

    }

    protected virtual void Start()
    {

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