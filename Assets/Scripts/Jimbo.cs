using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Jimbo : Gun
{
    public int bulletsPerShot = 5;
    public float spreadDegs = 5f;

    public override void fire(GameObject source)
    {
        if (canFire)
        {
            for (int i = 0; i < bulletsPerShot; i++)
            {
                // random rotation for each shot
                Quaternion rot = transform.rotation * Quaternion.Euler(0, 0, Random.Range(-spreadDegs, spreadDegs));
                GameObject bulletInstance = Instantiate(bullet, transform.position, rot);
                Bullet bulletScript = bulletInstance.GetComponent<Bullet>();
                bulletScript.source = source;
                bulletScript.speed = bulletScript.speed * Random.Range(0.9f, 1.1f);
            }
            
            bullet.GetComponent<Bullet>().source = source;
            timeOfLastFire = Time.time;
            canFire = false;
        }
    }
}
