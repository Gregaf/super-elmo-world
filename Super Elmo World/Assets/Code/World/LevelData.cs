using UnityEngine;

[CreateAssetMenu(fileName = "Level", menuName = "Over-World/GenericLevel", order = 1)]
public class LevelData : ScriptableObject
{
    public string levelName;
    [TextArea()]
    public string description;
    public SceneBuildInfo targetScene;
    public bool isLocked = true;

    
}
