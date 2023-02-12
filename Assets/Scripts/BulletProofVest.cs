using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BulletProofVest : Powerup
{
    public override void ApplyPowerup(Player player, Weapon weapon)
    {
        player.maxHealth *= 1.05f;
        player.health *= 1.05f;
    }
}