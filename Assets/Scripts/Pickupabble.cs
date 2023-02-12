using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public abstract class Pickupabble : MonoBehaviour
{
    public abstract Sprite icon {
        get;
    }
    public abstract bool Pickup(Player player);
}
