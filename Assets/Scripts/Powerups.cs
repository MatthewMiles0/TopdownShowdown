using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Powerups : MonoBehaviour
{
    public enum Types { FireRate, Damage, Speed, Knockback, Thorns, Burn, Slow, GunUpgrade, BulletStorm, Vaccine };
    [System.NonSerialized] public Dictionary<Types, Powerup> powerups = new Dictionary<Types, Powerup>();
    // private Dictionary<Types, int> powerupCounts = new Dictionary<Types, int>();

    public Player player;

    public Types referenceDELETEME;

    void Start()
    {
        foreach (Powerup powerup in GetComponentsInChildren<Powerup>())
        {
            powerups.Add(powerup.type, powerup);
        }

        foreach (Types type in System.Enum.GetValues(typeof(Types)))
        {
            // powerupCounts.Add(type, 0);
            if (!powerups.ContainsKey(type))
            {
                Debug.LogError("Powerups.cs: Powerup type " + type + " is missing.");
                powerups.Add(type, null);
            }
        }

        if (powerups.Count != System.Enum.GetNames(typeof(Types)).Length)
        {
            Debug.LogError("Powerups.cs: Powerup array length does not match number of powerup types.");
        }   
    }

    void Update()
    {
        // foreach (Types type in System.Enum.GetValues(typeof(Types)))
        // {
        //     if (powerupCounts[type] > 0)
        //     {
        //         powerups[type].ApplyPowerup(powerupCounts[type], player, player.hand.weapon);
        //     }
        // }
    }

    public void AddPowerup(Types type)
    {
        powerups[type].ApplyPowerup(player, player.hand.weapon);
        // powerupCounts[type]++;
    }
}
