using System;
using System.Diagnostics;

public enum GrowthStates
{
    Small,
    Big
}
public class GrowthEventArgs : EventArgs
{
    public GrowthStates growthState { get; private set; }
    public int itemID;

    // Will have a priority of 1: Small | 2: Big | 3: Projectile Mode, Cape, Ones that add actual functionality.
    public int growthLevel;

    public GrowthEventArgs()
    { 
        
    }

    public GrowthEventArgs(int level)
    {
        // When there isnt an item.
        growthLevel = level;
        itemID = -1;
    }

    public void SetGrowthState()
    {
        switch (growthLevel)
        {
            case 1:
                growthState = GrowthStates.Small;
                break;
            case 2:
                growthState = GrowthStates.Big;
                break;
            case 0:
                // Death so it will be ignored
                break;
            default:
                throw new Exception("Undefined State");
        }
    }

}
