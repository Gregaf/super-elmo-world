﻿

public class DestructibleBlock : Block
{
    

    protected override void Awake()
    {
        base.Awake();
    }

    protected override void Start()
    {
        base.Start();
    }

    protected override void Update()
    {
        base.Update();
    }

    public override void OnHitBlock()
    {
        base.OnHitBlock();
        StartCoroutine(BlockBounce());
    }
}