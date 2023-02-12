using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Collider2D))]
public class PickupabblePowerup : Pickupabble
{
    public Powerups powerups; 
    public Powerups.Types type;

    private Powerup powerup;
    
    [SerializeField] private SpriteRenderer _icon;

    public override Sprite icon {
        get {
            return _icon.sprite;
        }
    }

    void Start()
    {
        // _icon = Instantiate(iconPrefab, transform.position, Quaternion.identity, transform).GetComponent<SpriteRenderer>().sprite;
        if (powerups == null)
            powerups = FindObjectOfType<Powerups>();

        _icon.sprite = powerups.powerups[type].icon;

        powerup = powerups.powerups[type];
        
        if (powerup == null)
        {
            Debug.LogError("powerup does not exist");
        }
    }

    void Update()
    {
        
    }

    public override bool Pickup(Player player)
    {
        powerups.AddPowerup(type);

        Destroy(gameObject);
        return true;
    }
}
