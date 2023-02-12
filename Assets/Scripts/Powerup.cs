using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public abstract class Powerup : MonoBehaviour
{
    public Powerups.Types type;
    public Sprite icon;
    public abstract void ApplyPowerup(Player player, Weapon weapon);
}