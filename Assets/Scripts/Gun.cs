using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Gun : Weapon
{
    public GameObject bullet;
    public int clipSize = 10;
    public int currentClip = 10;
    public float fireDelay = 0.5f;

    protected float timeOfLastFire;

    void Update()
    {
        if (!canFire && (Time.time - timeOfLastFire) > fireDelay)
        {
            canFire = true;
        }
    }

    public override void fire(GameObject source)
    {
        if (canFire)
        {
            Instantiate(bullet, transform.position, transform.rotation);
            bullet.GetComponent<Bullet>().source = source;
            timeOfLastFire = Time.time;
            canFire = false;
        }
    }
}