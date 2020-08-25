using UnityEngine;

public class KeyGetUsed : IState
{
    private Key key;
    private KeyDoor targetDoor;
    private CharacterController2D controller2D;
    private float smoothTime;

    private Transform keyTransform;

    private Vector2 storeVector;

    public KeyGetUsed(Key key, KeyDoor targetDoor, CharacterController2D controller2D, float smoothTime)
    {
        this.key = key;
        this.targetDoor = targetDoor;
        this.controller2D = controller2D;
        this.smoothTime = smoothTime;

        keyTransform = key.transform;
    }

    public void Enter()
    {
        targetDoor.KeyBrought();
    }

    public void Exit()
    {
    }

    public void OnTriggerEnter(Collider2D collider2D)
    {
        throw new System.NotImplementedException();
    }

    public void Tick()
    {
        keyTransform.position = Vector2.SmoothDamp(keyTransform.position, targetDoor.transform.position, ref storeVector, smoothTime);

        if (Vector2.Distance(keyTransform.position, targetDoor.transform.position) < .25f)
        {
            ReachedDoor();
        }
    }

    private void ReachedDoor()
    {
        // Play effect
        GameObject.Destroy(key.gameObject);     
    }
    
}
