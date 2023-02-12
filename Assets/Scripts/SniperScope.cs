using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SniperScope : Powerup
{
    public override void ApplyPowerup(Player player, Weapon weapon)
    {
        if (weapon is Jimbo)
        {
            Jimbo jimbo = (Jimbo) weapon;
            jimbo.spreadDegs *= 0.85f;
        }

        if (weapon is Nail)
        {
            //TODO implement bullet chaining
        }

        if (weapon is Grenada)
        {
            Grenada grenada = (Grenada) weapon;
            grenada.bullet.GetComponent<Grenade>().explosionRadius *= 1.1f;
        }
    }
}