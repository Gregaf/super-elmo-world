using System;
using System.Diagnostics;

public enum GrowthStates
{
    Small,
    Big
}
public class PickUpEventArgs : EventArgs
{
    public int itemID;
    public int coinValue;
    public int scoreValue;
    public int livesValue;

    public PickUpEventArgs()
    {
        this.itemID = -1;
        this.coinValue = 0;
        this.scoreValue = 0;
        this.livesValue = 0;
    }

}
