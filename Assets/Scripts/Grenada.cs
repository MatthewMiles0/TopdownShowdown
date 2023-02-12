using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grenada : Gun
{
    // public override void fire(GameObject source)
    // {
    //     if (canFire)
    //     {
    //         Instantiate(bullet, transform.position, transform.rotation);
            
    //         // grenade specific
    //         Grenade grenade = bullet.GetComponent<Grenade>();
    //         grenade.source = source;
    //         grenade.GetComponent<Rigidbody2D>().AddForce(transform.right * grenade.speed, ForceMode2D.Impulse);

    //         timeOfLastFire = Time.time;
    //         canFire = false;
    //     }
    // }
}
