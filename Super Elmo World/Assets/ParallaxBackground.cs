using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParallaxBackground : MonoBehaviour
{
    [SerializeField] private float parallaxEffect = 0.5f;


    private Transform backgroundTransform;
    private Transform cameraTransform;
    private Vector3 lastCameraPosition;

    private void Start()
    {
        print("HEY!");
        backgroundTransform = transform;
        cameraTransform = Camera.main.transform;
        lastCameraPosition = cameraTransform.position;
    }

    private void Update()
    {
        Vector3 deltaMove = cameraTransform.position - lastCameraPosition;
        backgroundTransform.position += deltaMove * parallaxEffect;
        lastCameraPosition = cameraTransform.position;
    }

}
