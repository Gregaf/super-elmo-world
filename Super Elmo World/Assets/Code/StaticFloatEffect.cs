using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StaticFloatEffect : MonoBehaviour
{
    [Range(-1, 1)]
    public int inverter = 1;
    [SerializeField] private float alternateSpeed = 1f;
    [SerializeField] private float height = 1f;

    private Vector3 intialPosition;
    private Transform floatTransform;

    private void Start()
    {
        floatTransform = this.transform;
        intialPosition = transform.position;
    }

    private void Update()
    {
        float upDown = (Mathf.Sin(Time.time * alternateSpeed * inverter) * height);

        Vector3 newPosition = transform.position;

        newPosition.y = intialPosition.y + upDown;

        floatTransform.position = newPosition;
    }

}
