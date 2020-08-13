using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemManager : MonoBehaviour
{
    private const int maxNumberOfItems = 2;
    private FixedQueue<PowerUp> items;

    private void Awake()
    {
        items = new FixedQueue<PowerUp>(maxNumberOfItems);

    }

    private void OnEnable()
    {
        items.PushedOutElement += PushOutItem;
    }

    private void OnDisable()
    {
        items.PushedOutElement -= PushOutItem;
    }

    public void AddItem(PowerUp newItem)
    {
        items.Add(newItem);
    }

    public void PushOutItem(PowerUp itemPushedOut)
    { 
        // Find Item in a dictionary using Item ID, then spawn that item either above player or falling down through the map.
    }

}

public class FixedQueue<T>
{
    private Queue<T> queue;
    private int maxElements;

    public event Action<T> PushedOutElement;

    public FixedQueue()
    {
        queue = new Queue<T>();
        this.maxElements = 1;
    }

    public FixedQueue(int maxElements)
    {
        queue = new Queue<T>();
        this.maxElements = maxElements;
    }

    public void Add(T element)
    {
        if ((queue.Count + 1) > maxElements)
        {
            PushedOutElement.Invoke(queue.Dequeue());
        }

        queue.Enqueue(element);
    }

    public void Dequeue()
    {
        PushedOutElement?.Invoke(queue.Dequeue());
    }


    public void AdjustMaxQueueSize(int newMax)
    {
        this.maxElements = newMax;
    }

}