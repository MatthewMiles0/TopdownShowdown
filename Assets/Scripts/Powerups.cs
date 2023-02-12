using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Powerups : MonoBehaviour
{
    public enum Types { FireRate, DamageReduction, Speed, Knockback, Thorns, Burn, Slow, GunUpgrade, BulletStorm, Vaccine };
    [System.NonSerialized] public Dictionary<Types, Powerup> powerups = new Dictionary<Types, Powerup>();
    private Dictionary<Types, int> powerupCounts = new Dictionary<Types, int>();
    private Dictionary<Types, Text> powerupTexts = new Dictionary<Types, Text>();
    private Dictionary<Types, Image> powerupIcons = new Dictionary<Types, Image>();

    public Color activeColor = Color.white;
    public Color inactiveColor = Color.gray;

    public Player player;

    void Start()
    {
        foreach (Powerup powerup in GetComponentsInChildren<Powerup>())
        {
            powerups.Add(powerup.type, powerup);
            powerupTexts.Add(powerup.type, powerup.GetComponentInChildren<Text>());
            powerupIcons.Add(powerup.type, powerup.GetComponentInChildren<Image>());
        }

        foreach (Types type in System.Enum.GetValues(typeof(Types)))
        {
            powerupCounts.Add(type, 0);
            powerupTexts[type].enabled = false;
            powerupIcons[type].color = inactiveColor;
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
        powerupCounts[type]++;
        powerupTexts[type].enabled = true;
        powerupTexts[type].text = powerupCounts[type].ToString();
        powerupIcons[type].color = activeColor;
    }
}
