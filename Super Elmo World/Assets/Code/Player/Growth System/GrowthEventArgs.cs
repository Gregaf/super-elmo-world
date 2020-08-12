using System;


public class GrowthEventArgs : EventArgs
{
    public int currentHealth;
    public int growthID;

    public GrowthEventArgs(int currentHealth)
    {
        this.currentHealth = currentHealth;
        this.growthID = -1;
    }

    public GrowthEventArgs()
    {
        this.currentHealth = 1;
        this.growthID = -1;
    }


}
