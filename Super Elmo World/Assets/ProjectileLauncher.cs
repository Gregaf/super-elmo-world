using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileLauncher : MonoBehaviour
{

    public GameObject projectileToFire;
    public float launchSpeed;
    public float fireRate;
    [Range(-15, 15)]
    public float fireAngle;
    [Range(-1, 1)]
    public int direction;

    private Transform cannonTransform;
    [SerializeField] private Transform projectileSpawnLocation;
    private float fireTime;


    private void Awake()
    {
        if (projectileToFire.GetComponent<ILaunchable>() == null)
            throw new System.Exception($"{projectileToFire} is not of type {typeof(ILaunchable)}");

    }

    private void Start()
    {
        gameObject.transform.localScale = new Vector2(1, 1) * direction;
    }

    void Update()
    {
        if (fireTime >= fireRate)
        {
            
            GameObject fired = Instantiate(projectileToFire, projectileSpawnLocation.position, Quaternion.identity);
            fired.GetComponent<ILaunchable>().Launch(launchSpeed, direction);
            fireTime = 0;
        }

        fireTime += Time.deltaTime;
    }
}
