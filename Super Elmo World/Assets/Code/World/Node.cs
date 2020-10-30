using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Directions
{
    UP,
    DOWN,
    RIGHT,
    LEFT
}
public class Node : MonoBehaviour
{
    [SerializeField] private LevelData levelData;

    [Space(20)]
    [SerializeField] private Node upNode;
    [SerializeField] private Node downNode;
    [SerializeField] private Node rightNode;
    [SerializeField] private Node leftNode;

    Dictionary<Directions, Node> possiblePaths = new Dictionary<Directions, Node>();
    private void Awake()
    {
        possiblePaths.Add(Directions.UP, upNode);
        possiblePaths.Add(Directions.DOWN, downNode);
        possiblePaths.Add(Directions.RIGHT, rightNode);
        possiblePaths.Add(Directions.LEFT, leftNode);

    }

    
    public Node GetNode(Directions direction)
    {
        if(possiblePaths[direction] == null)
            Debug.Log($"The direction <color=yellow>'{direction}'</color> had no corresponding Node associated with it.");

        return possiblePaths[direction];
    }

    private void OnDrawGizmos()
    {
    }

}
