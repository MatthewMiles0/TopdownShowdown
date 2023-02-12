using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class IllegalGunParts : Powerup
{
    public override void ApplyPowerup(Player player, Weapon weapon)
    {
        Gun gun = weapon as Gun;
        gun.fireDelay *= 0.1f; // 0.95f
    }
}