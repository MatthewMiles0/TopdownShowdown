using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class PickupabbleWeapon : Pickupabble
{
    public GameObject weaponPrefab;
    private GameObject weaponInstance;
    private Weapon _weapon;

    public override Sprite icon {
        get {
            return _weapon.icon;
        }
    }

    void Start()
    {
        weaponInstance = Instantiate(weaponPrefab, transform.position, Quaternion.identity, transform);
        _weapon = weaponInstance.GetComponent<Weapon>();
        if (_weapon == null)
        {
            Debug.LogError("weaponPrefab does not have a Weapon component");
        }
    }

    void Update()
    {
        
    }

    public override bool Pickup(Player player)
    {
        int slot = player.inventory.nextEmptySlot;
        // Debug.Log("slot: "+slot);

        if (slot == -1) return false; // no slots available
        player.inventory.SetSlot(slot, _weapon);

        // reassign parent
        _weapon.transform.parent = player.hand.transform;

        // reset transform
        _weapon.transform.localRotation = Quaternion.identity;
        _weapon.transform.localPosition = Vector3.zero;
        _weapon.transform.localScale = Vector3.one;
        _weapon.gameObject.SetActive(false);

        Destroy(gameObject);
        return true;
    }
}
