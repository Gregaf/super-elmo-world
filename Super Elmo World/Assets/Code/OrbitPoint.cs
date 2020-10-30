using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrbitPoint : MonoBehaviour
{
    public float smoothTime = 0.25f;

    [Range(-1, 1)]
    public int invertValue = 1;

    [SerializeField] private Vector2 speed = Vector2.one;
    [SerializeField] private Vector2 amplitude = Vector2.one;
    [SerializeField] private Transform orbitTarget;

    private Transform objectTransform;
    private Vector3 store;


    private void Awake()
    {
        objectTransform = this.transform;

    }

    private void Update()
    {
        Vector3 newPosition = (transform.position);

        newPosition.x = orbitTarget.position.x + Mathf.Cos(Time.time * speed.x * invertValue) * amplitude.x;
        newPosition.y = orbitTarget.position.y + Mathf.Sin(Time.time * speed.y * invertValue) * amplitude.y;

        objectTransform.position = Vector3.SmoothDamp(objectTransform.position, newPosition, ref store, smoothTime);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;

        if(objectTransform != null && orbitTarget != null)
            Gizmos.DrawLine(objectTransform.position, orbitTarget.position);
    }
}
