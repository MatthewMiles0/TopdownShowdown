using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Weapon : MonoBehaviour
{
    public bool canFire = true;

    public Sprite icon;
    
    public abstract void fire(GameObject source);
}
