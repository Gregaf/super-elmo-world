using System;
using System.IO;
using UnityEngine;

public class PickUp : MonoBehaviour
{
    [SerializeField] protected string pickUpName = "default";
    [TextArea]
    [SerializeField] protected string description = "Does some stuff.";

    [Space(20)]
    [SerializeField] protected int coinValue = 0;
    [SerializeField] protected int scoreValue = 0;
    [SerializeField] protected int livesValue = 0;
    [SerializeField] protected AudioClip pickUpSound;
    

    protected virtual void Start()
    {
        ReadDescriptionFile();
    }

    protected virtual void Update()
    {
    }

    private void OnCollected(PlayerData playerData)
    {
        playerData.AddCoins(coinValue);
        playerData.AddScore(scoreValue);
        playerData.AddLives(livesValue);
    }

    protected virtual void OnTriggerEnter2D(Collider2D collider2D)
    {
        if (collider2D.GetComponent<PlayerData>() != null)
        {
            PlayerData playerData = collider2D.GetComponent<PlayerData>();

            OnCollected(playerData);

            Destroy(this.gameObject);
        }
    }

    // Reads the name and description from a text file that corresponds to the ItemID and writes to entityName and entityDescription.
    private void ReadDescriptionFile()
    {
        string filePath = $"Assets/ItemDescriptions/{pickUpName}.txt";

        try
        {
            // Throws fileNotFoundException if file doesn't Exist or path is incorrect.
            StreamReader streamReader = new StreamReader(filePath);

            description = streamReader.ReadToEnd();

            streamReader.Close();
        }
        catch
        {
            Debug.LogError($"File does not exist, try checking PickUpName is the same as txt file.");
        }
    }
}

