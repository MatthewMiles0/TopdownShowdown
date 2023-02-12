using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CanOBeans : Powerup
{
    public override void ApplyPowerup(Player player, Weapon weapon)
    {
        player.acceleration *= 1.05f;
    }
}