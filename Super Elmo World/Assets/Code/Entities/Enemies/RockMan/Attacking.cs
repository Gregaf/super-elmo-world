using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attacking : State
{
    private Rocker owner;
    private GameObject projectile;

    private int maxProjectiles = 3;

    private int nProjectilesThrown = 0;
    private float fireRate = .5f;
    private float fireTime;

    public Attacking(Rocker owner, GameObject projectile)
    {
        this.owner = owner;
        this.projectile = projectile;


    }

    public override void Enter()
    {
        nProjectilesThrown = 0;

        fireTime = Time.time + fireRate;
    }

    public override void Exit()
    {

    }

    public override void OnTriggerEnter2D(Collider2D collider2D)
    {
    }

    public override void Tick()
    {

        Debug.Log($"Time: {Time.time}, FireTime: {fireTime}");
        if (nProjectilesThrown < maxProjectiles && Time.time >= fireTime)
        {
            GameObject current = GameObject.Instantiate(projectile, owner.transform.position, Quaternion.identity);

            current.GetComponent<Projectile>().Launch(owner.target.position);

            fireTime = Time.time + fireRate;

            nProjectilesThrown++;
        }

        if (nProjectilesThrown >= maxProjectiles)
        { 
            // Transition from this state.
        }

    }
}
